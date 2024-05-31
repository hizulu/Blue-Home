using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FishController : MonoBehaviour
{
    [SerializeField] string currentState = "Idle";

    [Header("Colision")]
    private Rigidbody2D rb;
    private Animator anim;

    [Header("UI Sombra")]
    [SerializeField] private FishVideo videoPez;
    
    [Header("Enemy Stats")]
    float remainingIdleTime = 0f;
    [SerializeField] float maxIdleTime = 5f;
    [SerializeField] float minIdleTime = 2f;
    [SerializeField] float chaseDistance = 5f;
    [SerializeField] float tryAttackDistance = 1.5f;
    [SerializeField] float attackHitDistance = 1f;
    [SerializeField] float velocidad = 2f;
    float attackTime = 1.47f; // Esto es un numero magico, se deberia de cambiar

    [Header("Teleport")]
    float timeToTeleport = 5f;
    [SerializeField] float fadeInTime = 10f;
    [SerializeField] float fadeOutTime = 2f;
    [SerializeField] float maxTimeToTeleport = 15f;
    [SerializeField] float minTimeToTeleport = 5f;
    [SerializeField] float teleportTime = 2f;

    [Header("Sonido")]
    // TODO: Se deberia hacer con un solo audioSource y cambiar de clip de sonido 
    //  hay que añadir aquí el controlador de sonidos y que llame al index suyo
    //  public AudioSource audioSourcePersecucion;


    // Variables internas
    NavMeshAgent navMeshAgent;
    private GameObject[] puntosPatrullaje;
    private GameObject player;
    private Vector3 destino;
    SpriteRenderer spriteRenderer;
    bool isTeleporting = false;
    

    void Start()
    {
        // Inicializamos los componentes
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = velocidad;
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        puntosPatrullaje = GameObject.FindGameObjectsWithTag("Patrullaje");
        spriteRenderer = GetComponent<SpriteRenderer>();
        //audioSourcePersecucion.loop = true;

        // Inicializamos las variables
        remainingIdleTime = Random.Range(minIdleTime, maxIdleTime);
        destino = puntosPatrullaje[Random.Range(0, puntosPatrullaje.Length)].transform.position;
        timeToTeleport = Random.Range(minTimeToTeleport, maxTimeToTeleport);
    }

    // Update is called once per frame
    void Update()
    {
        if(isTeleporting) return;
        
        // Maquina de estados para el comportamiento del pez
        switch(currentState){
            case "Idle":
                Idle();
                break;
            case "Move":
                Move();
                break;
            case "Chase":
                Chase();
                break;
            case "Attack":
                Attack();
                break;
            case "Teleport":
                Teleport();
                break;
        }
    }
    
    private void Idle()
    {
        // Si el alpha es menor a 1 se hace un fade in
        if(spriteRenderer.color.a < 1f){
            spriteRenderer.color = Color.Lerp(spriteRenderer.color , new Color(1, 1, 1, 1), fadeInTime * Time.deltaTime);
        }

        // Se hace que se reproduzca la animacion de idle
        anim.SetFloat("CaminaHorz", 0f);
        anim.SetFloat("CaminaVert", 0f);

        // Si el jugador no esta cerca despues de un tiempo empezamos a movernos
        if (remainingIdleTime > 0)
        {
            remainingIdleTime -= Time.deltaTime;
        }
        else
        {
            destino = puntosPatrullaje[Random.Range(0, puntosPatrullaje.Length)].transform.position;
            timeToTeleport = Random.Range(minTimeToTeleport, maxTimeToTeleport);
            currentState = "Move";
        }

        // Si el jugador esta cerca empezamos a perseguir
        if(Vector2.Distance(transform.position, player.transform.position) < chaseDistance)
        {
            currentState = "Chase";
        }
    }

    void Move()
    {
        // Se hace que se reproduzca la animacion de move
        anim.SetFloat("CaminaHorz", -navMeshAgent.velocity.normalized.x);
        anim.SetFloat("CaminaVert", -navMeshAgent.velocity.normalized.y);

        // Se mueve hacia el punto
        navMeshAgent.SetDestination(destino);

        // Cuando llega al punto se vuelve a idle
        if(Vector2.Distance(transform.position, destino) < 0.1f)
        {
            remainingIdleTime = Random.Range(minIdleTime, maxIdleTime);
            currentState = "Idle";
        }

        // Si el jugador esta cerca empezamos a perseguir
        if(Vector2.Distance(transform.position, player.transform.position) < chaseDistance)
        {
            currentState = "Chase";
        }
    }

    private void Chase()
    {
        // TODO: añadir index poner sonido persecución
        /*if(audioSourcePersecucion.isPlaying)
        {
            StartCoroutine(FadeIn(audioSourcePersecucion, 1f));
        }
        */

        // Se hace que se reproduzca la animacion de chase
        anim.SetFloat("CaminaHorz", transform.position.x - player.transform.position.x);
        anim.SetFloat("CaminaVert", transform.position.y - player.transform.position.y);

        // Se persigue al jugador
        navMeshAgent.SetDestination(player.transform.position);

        // Si se pasa del tiempo de persecucion se teletransporta
        if (timeToTeleport > 0)
        {
            timeToTeleport -= Time.deltaTime;
        }
        else
        {
            currentState = "Teleport";
        }

        // Si el jugador se aleja mucho se vuelve a idle
        if(Vector2.Distance(transform.position, player.transform.position) > chaseDistance)
        {
            remainingIdleTime = Random.Range(minIdleTime, maxIdleTime);
            currentState = "Idle";
        }

        // Si el jugador esta muy cerca se ataca
        if(Vector2.Distance(transform.position, player.transform.position) < tryAttackDistance)
        {
            currentState = "Attack";
        }
    }

    /* 
    * TODO:
    * Esta funcion esta un poco hardcodeada, si se puede implementar por script mejor
    */
    private void Attack() 
    {
        // Se hace que se reproduzca la animacion de attack
        anim.SetFloat("CaminaHorz", transform.position.x - player.transform.position.x);
        anim.SetFloat("CaminaVert", transform.position.y - player.transform.position.y);
        anim.SetTrigger("Ataque");

        // Se persigue al jugador
        navMeshAgent.SetDestination(player.transform.position);

        // Si pasa el tiempo de ataque se comprueba otra vez si el jugador esta cerca
        if(attackTime > 0)
        {
            attackTime -= Time.deltaTime;
        }
        else
        {
            // Si el jugador esta cerca, se para al jugador, adelanta el tiempo y se teletransporta
            if(Vector2.Distance(transform.position, player.transform.position) < attackHitDistance)
            {
                videoPez.PlayVideo();
                player.GetComponent<CharacterController>().DetenerMovimiento();

                attackTime = 1.47f;
                currentState = "Teleport";
            }

            // Si no se vuelve a chase
            else
            {
                attackTime = 1.47f;
                currentState = "Chase";
            }
        }
    }
    private void Teleport()
    {
        // Se elige un punto aleatorio para teletransportarse, fuera del rango del jugador
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnSombra");
        GameObject spawnPoint;
        do
        {
            spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        } while (Vector2.Distance(destino, player.transform.position) < chaseDistance*3);

        // Hace un fade out
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, new Color(1, 1, 1, 0), fadeOutTime * Time.deltaTime);

        if(spriteRenderer.color.a < 0.1f)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0);
            // Se teletransporta
            transform.position = spawnPoint.transform.position;

            // Se detiene el navMeshAgent
            navMeshAgent.SetDestination(transform.position);

            // Se desactiva el comportamiento un tiempo
            isTeleporting = true;
            StartCoroutine(Teleporting());
            
            // vuelvo a idle
            remainingIdleTime = Random.Range(minIdleTime, maxIdleTime);
            currentState = "Idle";
        }  
    }

    IEnumerator Teleporting()
    {
        yield return new WaitForSeconds(teleportTime);
        isTeleporting = false;
    }
}
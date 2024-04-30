using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;

public class SombraController : MonoBehaviour
{
    [SerializeField] float velocidad = 2f;
    private NavMeshAgent _navMeshAgent;

    [Header("Colision")]
    private Rigidbody2D rb;
    private Animator _anim;

    [Header("ataque")]
    [SerializeField] private GameObject _hitBox;
    private GameObject _player;
    private bool canAtaque = true;
    [SerializeField, Range(0f, 3f)] private float disAtaque = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = velocidad;
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _anim = GetComponent<Animator>();
        _hitBox.SetActive(false);
        _player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
        posicionLayer();
        Ataque();
    }
    void Mover()
    {
        // Encuentra al jugador y obtiene sus posiciones
        GameObject personaje = GameObject.FindWithTag("Player");
        Vector2 posicionPersonaje = personaje.transform.position;
        Vector2 posicionSombra = transform.position;

        // Bandera para saber si el jugador ha sido visto alguna vez
        bool haVistoJugador = false;

        // Distancia para atacar
        float distanciaAtaque = disAtaque; 

        // Distancia para seguir (mayor que la distancia de ataque)
        float distanciaSeguir = 10f; 

        // Determina si el jugador está dentro del rango de seguimiento
        bool enRangoSeguir = Vector2.Distance(posicionSombra, posicionPersonaje) <= distanciaSeguir;

        if (enRangoSeguir)
        {
            // Marcarlo como visto
            haVistoJugador = true; 

            // Si el jugador esta en rango ir por el
            if (Vector2.Distance(posicionSombra, posicionPersonaje) <= distanciaAtaque)
            {
                _navMeshAgent.SetDestination(posicionPersonaje);
            }
            else
            {
                // El jugador no esta en rango, ir por el
                _navMeshAgent.SetDestination(posicionPersonaje);
            }
        }
        else
        {
            // El jugador no está en rango de seguimiento
            if (haVistoJugador)
            {
                // El jugador fue visto antes, pero ahora está fuera de rango
                // Sigue al jugador (hasta que se desvanezca o ataque)
                _navMeshAgent.SetDestination(posicionPersonaje);
            }
            else
            {
                // El jugador no ha sido visto todavía
                // Quédate quieto
                _navMeshAgent.SetDestination(posicionSombra);
            }
        }
        //animaciones
        _anim.SetFloat("CaminaHorz", posicionSombra.x - posicionPersonaje.x);
        _anim.SetFloat("CaminaVert", posicionSombra.y - posicionPersonaje.y);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //si el jugador toca a la sombra, se desvanece
            desaparecerAlosSegundos();
        }
    }
    void posicionLayer()
    {
            //override a la capa del player con su sprite
            if (transform.position.y-2 > GameObject.FindWithTag("Player").transform.position.y)
            {
                GetComponent<SpriteRenderer>().sortingOrder = 0;
            }
            else
            {
                GetComponent<SpriteRenderer>().sortingOrder = 2;
            }
        }
    private void Ataque()
    {
        //si puede atacar y está en rango
        if (canAtaque && Vector2.Distance(transform.position, GameObject.FindWithTag("Player").transform.position) < 1.5f)
        {
            //ataca
            canAtaque = false;
            _anim.SetTrigger("Ataque");
            _hitBox.SetActive(true);
            DireccionAtaque();
            Invoke("DesactivarAtaque", 1f);
        }
    }
    private void DireccionAtaque()
    {
        //haz que _hitbox gire respecto a la posición que esté el jugador 
        //que haga la animación correspondiente
        if (_anim.GetFloat("CaminaHorz") > 0)
        {
            _hitBox.transform.position = new Vector2(transform.position.x - disAtaque, transform.position.y);
        }
        else if (_anim.GetFloat("CaminaHorz") < 0)
        {
            _hitBox.transform.position = new Vector2(transform.position.x + disAtaque, transform.position.y);
        }
        else if (_anim.GetFloat("CaminaVert") > 0)
        {
            _hitBox.transform.position = new Vector2(transform.position.x, transform.position.y - disAtaque);
        }
        else if (_anim.GetFloat("CaminaVert") < 0)
        {
            _hitBox.transform.position = new Vector2(transform.position.x, transform.position.y + disAtaque);
        }
        else
        {
            _hitBox.transform.position = new Vector2(transform.position.x + 0.5f, transform.position.y);
        }   
    }
    private void DesactivarAtaque()
    {
        canAtaque = true;
        _hitBox.SetActive(false);
    }
    private void desaparecerAlosSegundos()
    {
        //evita que pueda llamar más de una vez a este codigo hasta que se teletransporte, de tipo trigger
        GetComponent<BoxCollider2D>().enabled = false;
        //empezar la corrutina una vez
        StartCoroutine("desvanecer");
        Invoke("teletransportarSombra",10f);
    }
    public void teletransportarSombra()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnSombra");
        GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        transform.position = spawnPoint.transform.position;
        GetComponent<BoxCollider2D>().enabled = true;
    }
    IEnumerable desvanecer()
    {
        //bajar el alpha del sprite progresivamente en 10 segundos
        for (float i = 1; i >= 0; i -= 0.1f)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.a = i;
            GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(1f);
        }
        teletransportarSombra();
    }
}

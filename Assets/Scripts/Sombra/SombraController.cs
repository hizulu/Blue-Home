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
    private bool estaAtacando = false;
    [SerializeField, Range(0f, 3f)] private float disAtaque = 3f;
    [SerializeField] float distanciaSeguir = 8f;

    [Header("Audio")]
    private AudioSource audioSource;
    private float distanciaMinima = 6f;

    [Header("UI Sombra")]
    [SerializeField] private uiSombra uiSombra;

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
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
        posicionLayer();
        if (!estaAtacando && Vector2.Distance(transform.position, _player.transform.position) < 2.5f)
        {
            Ataque();
        }
    }
    void Mover()
    {
        // Encuentra al jugador y obtiene sus posiciones
        GameObject personaje = GameObject.FindWithTag("Player");
        Vector2 posicionPersonaje = personaje.transform.position;
        Vector2 posicionSombra = transform.position;

        bool haVistoJugador = false;
        float distanciaAtaque = disAtaque; 


        // Determina si el jugador está dentro del rango de seguimiento
        bool enRangoSeguir = Vector2.Distance(posicionSombra, posicionPersonaje) <= distanciaSeguir;

        if (enRangoSeguir)
        {
            // Marcarlo como visto
            haVistoJugador = true;
            desaparecerAlosSegundos();

            // Si el jugador esta en rango ir por el
            if (Vector2.Distance(posicionSombra, posicionPersonaje) <= distanciaAtaque)
            {
                _navMeshAgent.SetDestination(posicionPersonaje);
                MusicaPersecucion(true);
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
                MusicaPersecucion(false);
            }
        }
        //animaciones
        _anim.SetFloat("CaminaHorz", posicionSombra.x - posicionPersonaje.x);
        _anim.SetFloat("CaminaVert", posicionSombra.y - posicionPersonaje.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            uiSombra.PlayVideo();
            Invoke("teletransportarSombra", 1f);
        }
    }
    void posicionLayer()
    {
            //override a la capa del player con su sprite
            if (transform.position.y-1 > GameObject.FindWithTag("Player").transform.position.y)
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
        // Calcula la posicion del _hitBox basándote en la posicion del monstruo y la direccion hacia el jugador
        Vector2 direccionAtaque = CalcularDireccionAtaque();
        Vector2 posicionHitBox = (Vector2)transform.position + direccionAtaque * 1;

        // Ajusta la posición del _hitBox
        _hitBox.transform.position = posicionHitBox;

        // Gira el _hitBox hacia el jugador
        RotarHitBox(direccionAtaque);

        //si puede atacar y esta en rango
        if (canAtaque)
        {
            // Ataca
            _anim.SetTrigger("Ataque");
            

            // Retraso antes de verificar el ataque exitoso, que así se anime que ataca
            Invoke("VerificarAtaqueExitoso", 1.5f);
        }
    }
    
    private Vector2 CalcularDireccionAtaque()
    {
        // Calcular la direccion del jugador respecto a la sombra
        Vector2 direccionJugador = _player.transform.position - transform.position;
        return direccionJugador.normalized;
    }
    private void RotarHitBox(Vector2 direccionAtaque)
    {
        // Calcular el angulo entre la dirección del jugador y los ejes principales
        float angulo = Mathf.Atan2(direccionAtaque.y, direccionAtaque.x) * Mathf.Rad2Deg;
        float anguloRedondeado = Mathf.Round(angulo / 45) * 45;

        _hitBox.transform.rotation = Quaternion.AngleAxis(anguloRedondeado, Vector3.forward);
    }
    private bool realizarAtaqueExitoso()
    {
        _hitBox.SetActive(true);
        // Determina si el jugador está en la direccion del ataque
        Vector2 direccionAtaque = _hitBox.transform.position - transform.position;
        Vector2 direccionJugador = GameObject.FindWithTag("Player").transform.position - transform.position;
        float angulo = Vector2.Angle(direccionAtaque, direccionJugador);

        if (angulo < 45)
        {
            return true;
        }
        return false;
    }
    void VerificarAtaqueExitoso()
    {
        // Verificar si el ataque es exitoso
        if (realizarAtaqueExitoso())
        {
            // Teletransportar después de un ataque exitoso y saltar scream
            Invoke("teletransportarSombra", 1f);
            uiSombra.PlayVideo();
            estaAtacando = true; // evita que pueda atacar al teletransportarse
        }
        Invoke("DesactivarAtaque", 0.5f);
    }

    private void DesactivarAtaque()
    {
        estaAtacando = false;
        _hitBox.SetActive(false);
    }
    private void desaparecerAlosSegundos()
    {
        //evita que pueda llamar mas de una vez a este codigo hasta que se teletransporte, de tipo trigger
        GetComponent<BoxCollider2D>().enabled = false;
        //empezar la corrutina una vez
        StartCoroutine("desvanecer");
        Invoke("teletransportarSombra",10f);
    }
    public void teletransportarSombra()
    {
        //hacer la sombra visible en caso de que no lo esté
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = 1;

        //teletransporte
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnSombra");
        GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        transform.position = spawnPoint.transform.position;
        GetComponent<BoxCollider2D>().enabled = true;
        //musica de persecucion
        MusicaPersecucion(false);

        //evitar el invoke de desaparecerAlosSegundos y que se repita de nuevo
        StopCoroutine("desvanecer");
        CancelInvoke("teletransportarSombra");

    }
    IEnumerable desvanecer()
    {
        // Obtener el color inicial
        Color color = GetComponent<SpriteRenderer>().color;
        float alphaDecrementStep = 0.1f / (10f / Time.deltaTime); // Decrementa en 0.1 durante 10 segundos

        while (color.a > 0)
        {
            // Decrementar el alpha
            color.a -= alphaDecrementStep;
            GetComponent<SpriteRenderer>().color = color;
            yield return null;
        }
        // Asegurarse de que el alpha sea exactamente 0
        color.a = 0;
        GetComponent<SpriteRenderer>().color = color;
    }

    public void MusicaPersecucion(bool iniciar)
    {
        // Si se indica iniciar la música
        if (iniciar)
        {
            // Iniciar la corrutina de la music
            StartCoroutine(sonidoSombra());
            Debug.Log("Iniciando la musica");
        }
        else
        {
            // Detener la corrutina de la music
            StopCoroutine(sonidoSombra());
            Debug.Log("Quitando la musica");
        }
    }
    private IEnumerator sonidoSombra()
    {
        // Bucle infinito para reproducir la música en bucle
        while (true)
        {
            // Ajustar el volumen de la música en función de la distancia
            float distanciaActualAlJugador = Vector2.Distance(transform.position, _player.transform.position);
            float volumenMusica = 1.0f - Mathf.Clamp01(distanciaActualAlJugador / distanciaMinima);
            audioSource.volume = volumenMusica;
            Debug.Log("musica" + volumenMusica);
             
            // Esperar medio segundo para mejorar la optimización
            yield return new WaitForSeconds(0.5f);
        }
    }
}
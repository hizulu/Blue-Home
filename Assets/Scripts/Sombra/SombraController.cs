using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        //Encuentre al personaje
        GameObject personaje = GameObject.FindWithTag("Player");
        //Obtenga la posición 
        Vector2 posicionPersonaje = personaje.transform.position;
        //Obtenga la posición de la sombra
        Vector2 posicionSombra = transform.position;

        //Vaya hacia el jugador

        _navMeshAgent.SetDestination(posicionPersonaje);


        //quiero movimiento usando los navmesh agent

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
    }
}

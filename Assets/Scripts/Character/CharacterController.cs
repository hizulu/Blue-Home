using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private bool interactuable = false;
    private List<IInteractuable> objetosInteractuables = new List<IInteractuable>();
    private List<GameObject> objetosADesactivar = new List<GameObject>();
    
    private Rigidbody2D _rig;
    private Animator _anim;

    [SerializeField] private float velocidad;
    [SerializeField] private float recoveryTime = 0f;
    [SerializeField] public GameObject marcaOpcionInteraccion;

    public List<GameObject> ObjetosADesactivar { get => objetosADesactivar; set => objetosADesactivar = value; }


    private void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Movimiento();
    }
    private void Update()
    {
        //Esto en update que llame a los objetos en rango para interactuar
        if (interactuable)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                foreach (var interactuable in objetosInteractuables)
                {
                    marcaOpcionInteraccion.SetActive(false);
                    interactuable.Interactuar();
                }
                foreach(var objeto in objetosADesactivar)
                {
                    objeto.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Esto en trigger para saber si el objeto es interactuable
        IInteractuable objetoInteractuable = collision.GetComponent<IInteractuable>();
        if (objetoInteractuable != null)
        {
            marcaOpcionInteraccion.SetActive(true);
            interactuable = true;
            objetosInteractuables.Add(objetoInteractuable);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Esto en trigger para saber si el objeto ya no es interactuable
        IInteractuable objetoInteractuable = collision.GetComponent<IInteractuable>();
        if (objetoInteractuable != null)
        {
            marcaOpcionInteraccion.SetActive(false);
            interactuable = false;
            objetosInteractuables.Remove(objetoInteractuable);
        }
    }

    private void Movimiento()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (recoveryTime > 0)
        {
            recoveryTime -= Time.deltaTime;
            return;
        }

        Vector2 direccion = new Vector2(horizontal, vertical);

        if (direccion.magnitude > 1)
        {
            direccion = direccion.normalized;
        }

        _rig.velocity = direccion * velocidad;

        if (horizontal != 0 || vertical != 0)
        {
            _anim.SetFloat("CaminaHorz", horizontal);
            _anim.SetFloat("CaminaVert", vertical);
            _anim.SetBool("Camina", true);
        }
        else
        {
            _anim.SetBool("Camina", false);
        }
    }

    public void DetenerMovimiento()
    {
        _anim.SetBool("Camina", false);
        recoveryTime = 5f;
        _rig.velocity = Vector2.zero;
        GetComponent<SpriteRenderer>().color = Color.gray;
        Invoke("ColorOriginal", 2f);
    }

    private void ColorOriginal()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
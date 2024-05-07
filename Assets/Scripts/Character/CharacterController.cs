using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;

public class CharacterController : MonoBehaviour
{
    [Header("Configuracion de Movimiento")]
    [SerializeField]private float velocidad;
    [SerializeField] private float recoveryTime = 0f;

    [Header("Colision")]
    private Rigidbody2D _rig;
    private Animator _anim;

    [Header("UI Sombra")]
    [SerializeField] private FishVideo uiSombra;

    //llama al rig y lo guarda para realizar colisiones
    private void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        //esto es para cuando establezcamos un punto donde queramos que spawnee

        //puntoInicial = GameObject.Find("PuntoInicial").transform.position;  
        //gameObject.transform.position = puntoInicial;
    }

    private void FixedUpdate()      //siempre irá a la misma velocidad
    {
        Movimiento();
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

        Vector2 direccion = new Vector2(horizontal, vertical); //Los personajes no deben moverse más rápido en diagonal

        if (direccion.magnitude > 1)
        {
            direccion = direccion.normalized;
        }

        _rig.velocity = direccion * velocidad;

        if (horizontal != 0 || vertical != 0) //esto cambia las animaciones
        {
            _anim.SetFloat("CaminaHorz", horizontal);
            _anim.SetFloat("CaminaVert", vertical);

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
    void ColorOriginal()    //por si queremos que el personaje se paralice vaya
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

}

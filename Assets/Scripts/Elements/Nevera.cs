using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nevera : MonoBehaviour, IInteractuable
{
    //pillame dos objetos de la lista y los cambias dependiendo si está en rango el player
    [SerializeField] private GameObject[] objetos;
    [SerializeField] private GameObject ImagenTartaAdquirida;
    [SerializeField] private ActiveVideoAdquirido ActiveVideoAdquirido;
    GameObject Jugador;
    [SerializeField] private GameObject TartaEnMesa;
    [SerializeField] private GameObject TriggerDialogo;

    void Start()
    {
        Jugador = GameObject.FindWithTag("Player");
    }

    public void Interactuar()
    {
        //si la tarta no ha sido pillada
        if (!VariablesEstaticas.recogidolv4)
        {
            if (this.gameObject.GetComponent<Collider2D>().IsTouching(Jugador.GetComponent<Collider2D>()))
            {
                Debug.Log("Se ha tomado la comida");
                VariablesEstaticas.recogidolv4 = true;
                ImagenTartaAdquirida.SetActive(true);
                ActiveVideoAdquirido.PlayVideo();
                Jugador.GetComponent<CharacterController>().ObjetosADesactivar.Add(this.gameObject);
                TartaEnMesa.SetActive(true);
                TriggerDialogo.SetActive(false);
            }
        }
    }

    //Esto es para dejar bonita la nevera
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CambiarEstado();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CambiarEstado();
        }
    }
    public void CambiarEstado()
    {
        objetos[0].SetActive(!objetos[0].activeSelf);
        objetos[1].SetActive(!objetos[1].activeSelf);
    }
}

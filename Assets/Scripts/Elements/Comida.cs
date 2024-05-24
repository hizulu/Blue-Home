using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComidaScript : MonoBehaviour, IInteractuable
{
    //variables de referencia
    [SerializeField] private GameObject ImagenComidaAdquirida;
    [SerializeField] private ActiveVideoAdquirido ActiveVideoAdquirido;
    GameObject Jugador;
    [SerializeField] private GameObject TriggerComida;
    [SerializeField] private GameObject TriggerDialogo;

    void Start()
    {
        Jugador = GameObject.FindWithTag("Player"); 
    }

    public void Interactuar()
    {
        //si la comida del pez no ha sido recogida
        if (!VariablesEstaticas.recogidolv1)
        {
            if (this.gameObject.GetComponent<Collider2D>().IsTouching(Jugador.GetComponent<Collider2D>()))
            {
                Debug.Log("Se ha tomado la comida");
                VariablesEstaticas.recogidolv1 = true;
                ImagenComidaAdquirida.SetActive(true);
                ActiveVideoAdquirido.PlayVideo();
                Jugador.GetComponent<CharacterController>().ObjetosADesactivar.Add(this.gameObject);
                TriggerComida.SetActive(true);
                TriggerDialogo.SetActive(false);
            }
        }
    }
}
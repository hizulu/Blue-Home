using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComidaScript : MonoBehaviour, IInteractuable
{
    //variables de referencia
    [SerializeField] private GameObject ImagenInventario;
    GameObject Jugador;
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
                ImagenInventario.SetActive(true);
                Jugador.GetComponent<CharacterController>().ObjetosADesactivar.Add(this.gameObject);
            }
        }
    }
}
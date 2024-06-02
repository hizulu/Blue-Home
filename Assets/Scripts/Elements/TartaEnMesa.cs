using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TartaEnMesa : MonoBehaviour, IInteractuable
{
    //Activame la tarta en Interactuable y desactiva el objeto 
    [SerializeField] private GameObject Tarta;
    [SerializeField] private GameObject ImagenTartaAdquirida;
    GameObject Jugador;
    public void Start()
    {
        Jugador = GameObject.FindWithTag("Player");
    }
    public void Interactuar()
    {
        if (VariablesEstaticas.recogidolv4)
        {
            Tarta.SetActive(true);
            ImagenTartaAdquirida.SetActive(false);
            //Espera 5 segundos antes de pasar a la siguiente escena
            StartCoroutine(Esperar());
        }
    }
    IEnumerator Esperar()
    {
        yield return new WaitForSeconds(5);
        GameManager.instance.CargarNivel(9);
    }
}


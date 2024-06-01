using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basura : MonoBehaviour, IInteractuable
{
    [SerializeField] private GameObject ImagenBasuraAdquirida;
    private GameObject Jugador;
    [SerializeField] GameObject[] basuraDesactivar;
    public bool inventarioLleno = false;
    public static Basura instance { get; private set; }

    void Start()
    {
        Jugador = GameObject.FindWithTag("Player");
    }

    public void Interactuar()
    {
        if (this.gameObject.GetComponent<Collider2D>().IsTouching(Jugador.GetComponent<Collider2D>()))
        {
            if (inventarioLleno == false)
            {
                Debug.Log("Se ha tomado la basura");
                VariablesEstaticas.basuraRecolectada++;
                ImagenBasuraAdquirida.SetActive(true);
                DesactivarBasura();
                inventarioLleno = true;
            }
            if (VariablesEstaticas.basuraRecolectada >= VariablesEstaticas.basuraTotal)
            {
                VariablesEstaticas.completadolv1 = true;
            }
        }
    }
    public void DesactivarBasura() 
    {
        foreach (var objeto in basuraDesactivar)
        {
            Jugador.GetComponent<CharacterController>().ObjetosADesactivar.Add(this.gameObject);

        }
    }
}
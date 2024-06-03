using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basura : MonoBehaviour, IInteractuable
{
    [SerializeField] private GameObject ImagenBasuraAdquirida;
    [SerializeField] private GameObject Jugador;
    public static Basura instance { get; private set; }
    public void Interactuar()
    {
        if (!VariablesEstaticas.inventarioLleno)
        {
            Debug.Log("Se ha tomado la basura");
            VariablesEstaticas.basuraRecolectada++;
            ImagenBasuraAdquirida.SetActive(true);
            VariablesEstaticas.CalcetinBasura = 1;
            VariablesEstaticas.inventarioLleno = true;
            DesactivarBasura();
        }
        if (Cestodelaropa.ropaTotal + CubosBasura.basuraTotal >= VariablesEstaticas.basuraTotal)
        {
            VariablesEstaticas.completadolv2 = true;
            GameManager.instance.CargarNivel(4);
        }

    }
    public void DesactivarBasura()
    {
        gameObject.SetActive(false);
    }
}
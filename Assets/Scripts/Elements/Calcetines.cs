using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calcetines : MonoBehaviour, IInteractuable
{
    [SerializeField] private GameObject ImageRopaAdquirida;
    [SerializeField] private GameObject Jugador;
    public static Calcetines instance { get; private set; }
    public void Interactuar()
    {
        if (!VariablesEstaticas.inventarioLleno)
        {
            Debug.Log("Se ha tomado la basura");
            VariablesEstaticas.basuraRecolectada++;
            ImageRopaAdquirida.SetActive(true);

            VariablesEstaticas.CalcetinBasura = -1;
            VariablesEstaticas.inventarioLleno = true;
            DesactivarCalcetines();
        }
        if (VariablesEstaticas.basuraRecolectada >= VariablesEstaticas.basuraTotal)
        {
            VariablesEstaticas.completadolv2 = true;
        }
    }
    public void DesactivarCalcetines()
    {
        gameObject.SetActive(false);
    }
}
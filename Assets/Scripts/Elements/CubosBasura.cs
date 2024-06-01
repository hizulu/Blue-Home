using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubosBasura : MonoBehaviour, IInteractuable
{
    [SerializeField] private GameObject textoMision;
    [SerializeField] private GameObject ImagenInventario;
    private int basuraEnCubo = 0;
    private int basuraTotal = 0;

    public void Interactuar()
    {
        if (!VariablesEstaticas.completadolv1 && Basura.instance.inventarioLleno)
        {
            basuraEnCubo++;
            basuraTotal++;
            ImagenInventario.SetActive(false);
            Basura.instance.inventarioLleno = false;


            if (basuraEnCubo >= 3)
            {
                Debug.Log("Cubo de basura lleno");
                ImagenInventario.SetActive(false);
                basuraEnCubo = 0;
                VariablesEstaticas.recogidolv1 = false;
            }
        }
        if (basuraTotal==VariablesEstaticas.basuraTotal)
        {
            VariablesEstaticas.completadolv1 = true;
            textoMision.SetActive(false);
            ImagenInventario.SetActive(false);
        }
    }
}
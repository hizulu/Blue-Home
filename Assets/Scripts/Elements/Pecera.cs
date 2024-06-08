using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pecera : MonoBehaviour, IInteractuable
{
    //variables de referencia
    [SerializeField] GameObject textoMision;
    [SerializeField] GameObject ImagenInventario;

    public void Interactuar()
    {
        if (VariablesEstaticas.recogidolv1)
        {
            //sencillo, asi desde el GameManager se puede saber si se ha completado el nivel
            Debug.Log("Completado el 1er nivel");
            textoMision.SetActive(false);
            ImagenInventario.SetActive(false);
            VariablesEstaticas.completadolv1 = true;
        }
        if (VariablesEstaticas.completadolv1)
        {
            textoMision.SetActive(false);
            ImagenInventario.SetActive(false);
            GameManager.instance.CargarNivel(3);
        }
    }
}
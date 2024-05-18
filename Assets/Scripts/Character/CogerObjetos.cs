using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CogerObjetos : MonoBehaviour
{
    //pudiera hacer un prefab de comidas donde solo se inicialice con una lista de posiciones, pero esto es mas sencillo

    //Variables de comida del pez
    GameObject[] ComidaPezVaria;
    GameObject ComidaPez;

    //variables de referencia
    [SerializeField] GameObject textoMision;
    [SerializeField] GameObject ImagenInventario;

    private void Start()
    {
        ComidaPezVaria = GameObject.FindGameObjectsWithTag("ComidaPez");
        foreach (GameObject comida in ComidaPezVaria)
        {
            comida.SetActive(false);
        }
        ComidaPez = ComidaPezVaria[Random.Range(0, ComidaPezVaria.Length)];
        ComidaPez.SetActive(true);
        textoMision.SetActive(true);
    }
}
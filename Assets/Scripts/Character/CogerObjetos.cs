using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogerObjetos : MonoBehaviour, IInteractuable
{
    [SerializeField] GameObject ComidaPez;
    [SerializeField] GameObject Pecera;
    [SerializeField] GameObject textoMision;
    bool recogido = false;
    public bool completado = false;

    public static CogerObjetos instance { get; private set; }
    public void Interactuar()
    {
        if (gameObject.CompareTag("Tarea1") && !recogido) // Solo interactuar si no ha sido recogido
        {
            Debug.Log("Has recogido un objeto");
            ComidaPez.SetActive(true);
            recogido = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == Pecera && recogido) // Solo si el otro objeto es la pecera y ha sido recogido
        {
            Debug.Log("Comida de pez usada");
            completado = true;
            ComidaPez.SetActive(false);
            textoMision.SetActive(false);
            recogido = false;
            gameObject.SetActive(false);
        }
    }
}

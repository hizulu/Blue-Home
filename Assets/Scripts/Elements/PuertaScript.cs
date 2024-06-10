using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaScript : MonoBehaviour, IInteractuable
{
    [SerializeField] GameObject puerta;
    public void Interactuar()
    {
        Debug.Log("Puerta abierta");
        //pasa a la siguiente escena respecto a esta usando el game manager
        GameManager.instance.CargarNivel(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // si hay puerta
        if (puerta != null && puerta.activeSelf)
        {
            puerta.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // esto deberia evitar errores
        if (puerta != null && !puerta.activeSelf)
        {
            puerta.SetActive(true);
        }
    }
}   
using System.Collections;
using UnityEngine;
using TMPro;

public class Objetosdialogo : MonoBehaviour
{
    [SerializeField] public Collider2D colliderObjeto;
    [SerializeField] public GameObject marcaOpcionInteraccion;
    [SerializeField] GameObject panelDialogoObjeto;
    [SerializeField] TMP_Text textoDialogoObjeto;
    [SerializeField, TextArea(4,6)] public string[] lineasDialogoObjeto;

    private float tiempoEntreLetras = 0.05f;
    private bool jugadorCerca = false;
    private bool dialogoActivo;
    private int indexLinea;

    void Update()
    {
        if (jugadorCerca && Input.GetButtonDown("Fire1")) // Si el jugador está cerca y presiona el botón de interacción
        {
            if (!dialogoActivo)
            {
                ActivarDialogo();
            }
            else 
            {               
                 StopAllCoroutines();
                 textoDialogoObjeto.text = lineasDialogoObjeto[indexLinea];          
                if(Input.GetButtonDown("Fire1"))
                {
                    DesactivarDialogo();
                }
            }
        }
    }

    private void ActivarDialogo() // Activa el diálogo, pausa el tiempo y muestra las líneas de diálogo
    {
        dialogoActivo = true;
        panelDialogoObjeto.SetActive(true);
        marcaOpcionInteraccion.SetActive(false);
        indexLinea = UnityEngine.Random.Range(0, lineasDialogoObjeto.Length);
        Time.timeScale = 0f;
        StartCoroutine(MostrarLineasDialogo());
    }

    private void DesactivarDialogo() // Desactiva el diálogo y reanuda el tiempo
    {
        panelDialogoObjeto.SetActive(false);
        marcaOpcionInteraccion.SetActive(true);
        dialogoActivo = false;
        Time.timeScale = 1f;
    }


    private IEnumerator MostrarLineasDialogo()
    {
        textoDialogoObjeto.text = string.Empty;
        foreach (char letra in lineasDialogoObjeto[indexLinea].ToCharArray())
        {
            textoDialogoObjeto.text += letra;
            yield return new WaitForSecondsRealtime(tiempoEntreLetras);
        }
    }

    private void OnTriggerEnter2D(Collider2D colliderObjeto)
    {
        if (colliderObjeto.gameObject.CompareTag("Player"))
        {
            jugadorCerca = true;
            marcaOpcionInteraccion.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D colliderObjeto)
    {
        if (colliderObjeto.gameObject.CompareTag("Player"))
        {
            jugadorCerca = false;
            marcaOpcionInteraccion.SetActive(false);
        }
    }
}

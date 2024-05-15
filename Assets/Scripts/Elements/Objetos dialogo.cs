using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Objetosdialogo : MonoBehaviour, IInteractuable
{
    [Header("Elementos del dialogo")]
    [SerializeField] public Collider2D colliderObjeto;
    [SerializeField] public GameObject marcaOpcionInteraccion;
    [SerializeField] GameObject panelDialogoObjeto;
    [SerializeField] public Image imagenIntermitente;
    [SerializeField] TMP_Text textoDialogoObjeto;
    [SerializeField] public Image[] imagenesDialogoObjeto;

    [Header("Variables activas por objeto")]
    [SerializeField, TextArea(4, 6)] public string[] lineasDialogoObjeto;

    public float tiempoEntreLetras = 0.2f;
    private bool dialogoActivo;
    private int indexLinea;

    public void Interactuar()
    {
        if (!dialogoActivo)
        {
            ActivarDialogo();
        }
        else
        {
            // Para cuando se reactiva el dialogo
            StopAllCoroutines();
            textoDialogoObjeto.text = lineasDialogoObjeto[indexLinea];
            foreach (var imagen in imagenesDialogoObjeto)
            {
                imagen.gameObject.SetActive(false);
            }
            imagenIntermitente.gameObject.SetActive(true);

            indexLinea++; // Pasar al siguiente di�logo
            if (indexLinea >= lineasDialogoObjeto.Length)
            {
                // Si no hay m�s di�logos, desactivar el panel de di�logo y reanudar el tiempo
                panelDialogoObjeto.SetActive(false);
                marcaOpcionInteraccion.SetActive(true);
                imagenIntermitente.gameObject.SetActive(false);
                dialogoActivo = false;
                Time.timeScale = 1f;
            }
        }
    }

    private void ActivarDialogo() // Activa el di�logo, pausa el tiempo y muestra las l�neas de di�logo
    {
        dialogoActivo = true;
        panelDialogoObjeto.SetActive(true);
        marcaOpcionInteraccion.SetActive(false);
        indexLinea = UnityEngine.Random.Range(0, lineasDialogoObjeto.Length);
        Time.timeScale = 0f;

        // Resetear el texto del di�logo
        textoDialogoObjeto.text = string.Empty;

        // Activar todas las im�genes de di�logo al iniciar un nuevo di�logo
        foreach (var imagen in imagenesDialogoObjeto)
        {
            imagen.gameObject.SetActive(true);
        }

        StartCoroutine(MostrarLineasDialogo());
    }


    private void DesactivarDialogo() // Desactiva el di�logo y reanuda el tiempo
    {
        panelDialogoObjeto.SetActive(false);
        marcaOpcionInteraccion.SetActive(true);
        dialogoActivo = false;
        Time.timeScale = 1f;
    }

    private IEnumerator MostrarLineasDialogo()
    {
        // Variable para controlar el tiempo de cambio de imagen
        float tiempoCambioImagen = Random.Range(0.5f, 1.5f); 
        int indexImagen = 0;

        // Ciclo para mostrar las l�neas de di�logo letra por letra
        foreach (char letra in lineasDialogoObjeto[indexLinea].ToCharArray())
        {
            // Mostrar la letra actual
            textoDialogoObjeto.text += letra;
            tiempoCambioImagen -= tiempoEntreLetras;

            // Si es hora de cambiar de imagen
            if (tiempoCambioImagen <= 0)
            {
                // Cambiar a la siguiente imagen de di�logo
                imagenesDialogoObjeto[indexImagen].gameObject.SetActive(false);
                indexImagen = (indexImagen + 1) % imagenesDialogoObjeto.Length;
                imagenesDialogoObjeto[indexImagen].gameObject.SetActive(true);

                tiempoCambioImagen = Random.Range(0.5f, 1.5f);
            }
            yield return new WaitForSecondsRealtime(tiempoEntreLetras);
        }

        // Una vez que el di�logo ha terminado de escribirse, desactivar todas las im�genes de di�logo
        foreach (var imagen in imagenesDialogoObjeto)
        {
            imagen.gameObject.SetActive(false);
        }

        imagenIntermitente.gameObject.SetActive(true);
    }

}

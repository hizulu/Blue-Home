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

            indexLinea++; // Pasar al siguiente diálogo
            if (indexLinea >= lineasDialogoObjeto.Length)
            {
                // Si no hay más diálogos, desactivar el panel de diálogo y reanudar el tiempo
                panelDialogoObjeto.SetActive(false);
                marcaOpcionInteraccion.SetActive(true);
                imagenIntermitente.gameObject.SetActive(false);
                dialogoActivo = false;
                Time.timeScale = 1f;
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

        // Resetear el texto del diálogo
        textoDialogoObjeto.text = string.Empty;

        // Activar todas las imágenes de diálogo al iniciar un nuevo diálogo
        foreach (var imagen in imagenesDialogoObjeto)
        {
            imagen.gameObject.SetActive(true);
        }

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
        // Variable para controlar el tiempo de cambio de imagen
        float tiempoCambioImagen = Random.Range(0.5f, 1.5f); 
        int indexImagen = 0;

        // Ciclo para mostrar las líneas de diálogo letra por letra
        foreach (char letra in lineasDialogoObjeto[indexLinea].ToCharArray())
        {
            // Mostrar la letra actual
            textoDialogoObjeto.text += letra;
            tiempoCambioImagen -= tiempoEntreLetras;

            // Si es hora de cambiar de imagen
            if (tiempoCambioImagen <= 0)
            {
                // Cambiar a la siguiente imagen de diálogo
                imagenesDialogoObjeto[indexImagen].gameObject.SetActive(false);
                indexImagen = (indexImagen + 1) % imagenesDialogoObjeto.Length;
                imagenesDialogoObjeto[indexImagen].gameObject.SetActive(true);

                tiempoCambioImagen = Random.Range(0.5f, 1.5f);
            }
            yield return new WaitForSecondsRealtime(tiempoEntreLetras);
        }

        // Una vez que el diálogo ha terminado de escribirse, desactivar todas las imágenes de diálogo
        foreach (var imagen in imagenesDialogoObjeto)
        {
            imagen.gameObject.SetActive(false);
        }

        imagenIntermitente.gameObject.SetActive(true);
    }

}

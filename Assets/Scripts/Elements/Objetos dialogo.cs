using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Objetosdialogo : MonoBehaviour, IInteractuable
{
    [Header("Elementos del dialogo")]

    [SerializeField] public GameObject marcaOpcionInteraccion;
    [SerializeField] GameObject panelDialogoObjeto;
    [SerializeField] public Image imagenIntermitente;
    [SerializeField] TMP_Text textoDialogoObjeto;
    [SerializeField] public Image[] imagenesDialogoObjeto;

    [Header("Variables activas por objeto")]
    [SerializeField, TextArea(4, 6)]
    public string[] lineasDialogoObjeto;

    public float tiempoEntreLetras = 0.2f;
    private bool dialogoActivo;
    private int indexLinea;
    private BoxCollider2D BoxCollider2D;

    private void Start()
    {
        BoxCollider2D = GetComponent<BoxCollider2D>();
    }
    public void Interactuar()
    {
        if (!dialogoActivo) // Si no hay dialogo activo y no se ha leido el dialogo
        {
            dialogoActivo = true; // Establecer dialogoActivo a true cuando se inicia el di logo
            ActivarDialogo();
        }
        else if (dialogoActivo &&
                 textoDialogoObjeto.text == lineasDialogoObjeto[indexLinea]) // Si ya hay un dialogo activo
        {
            if (lineasDialogoObjeto[indexLinea].Contains(""))
            {
                textoDialogoObjeto.text = "";
                FinalizarDialogo();
            }
            else
            {
                SiguienteLineaDialogo();
            }
        }
        else
        {
            StopAllCoroutines();
            textoDialogoObjeto.text = lineasDialogoObjeto[indexLinea];
            imagenesDialogoObjeto[0].gameObject.SetActive(true);
        }
    }

    private void ActivarDialogo() // Activa el dialogo, pone el tiempo en pausa y muestra las lineas de dialogo
    {
        panelDialogoObjeto.SetActive(true);
        imagenIntermitente.gameObject.SetActive(false); // Desactivar la imagen intermitente al iniciar el dialogo
        indexLinea = 0;
        Time.timeScale = 0f;
        StartCoroutine(MostrarLineasDialogo());
    }

    private void SiguienteLineaDialogo() // Muestra la siguiente linea de dialogo
    {
        textoDialogoObjeto.text = ""; // Coco es un juego de palabras (Lo hace ver mas pro los dialogos)
        indexLinea++;
        if (indexLinea < lineasDialogoObjeto.Length)
        {
            StopCoroutine(MostrarLineasDialogo());
            imagenIntermitente.gameObject.SetActive(false);
            StartCoroutine(MostrarLineasDialogo());
        }
        else // Si ya no hay mas lineas de dialogo, se desactiva el panel de dialogo y se reactiva la opcion de interaccion
        {
            FinalizarDialogo();
        }
    }

    void FinalizarDialogo()
    {
        panelDialogoObjeto.SetActive(false);
        //marcaOpcionInteraccion.SetActive(true);
        dialogoActivo = false;

        Time.timeScale = 1f;
        imagenesDialogoObjeto[0].gameObject.SetActive(true);
        imagenIntermitente.gameObject.SetActive(false); // Desactivar la imagen intermitente al finalizar el di logo   
    }

    private IEnumerator MostrarLineasDialogo()
    {
        // Variable para controlar el tiempo de cambio de imagen
        float tiempoCambioImagen = Random.Range(0.2f, 1f);
        int indexImagen = 0;

        // Ciclo para mostrar las lineas de dialogo letra por letra
        foreach (char letra in lineasDialogoObjeto[indexLinea])
        {
            // Mostrar la letra actual
            textoDialogoObjeto.text += letra;
            tiempoCambioImagen -= tiempoEntreLetras;

            // Si es hora de cambiar de imagen
            if (tiempoCambioImagen <= 0)
            {
                // Cambiar a la siguiente imagen de di logo
                imagenesDialogoObjeto[indexImagen].gameObject.SetActive(false);
                indexImagen = (indexImagen + 1) % imagenesDialogoObjeto.Length;
                imagenesDialogoObjeto[indexImagen].gameObject.SetActive(true);

                tiempoCambioImagen = Random.Range(0.2f, 1f);
            }

            yield return new WaitForSecondsRealtime(tiempoEntreLetras);
        }
        imagenIntermitente.gameObject.SetActive(true);
    }
}
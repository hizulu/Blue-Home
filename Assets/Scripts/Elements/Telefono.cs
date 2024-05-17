using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Telefono : MonoBehaviour, IInteractuable
{
    [Header("Elementos del dialogo")]
    [SerializeField] public Collider2D colliderTelefono;
    [SerializeField] public GameObject marcaOpcionInteraccion;
    [SerializeField] GameObject panelDialogoTelefono;
    [SerializeField] public Image[] imagenesDialogoObjeto;
    [SerializeField] public Image imagenIntermitente;

    [SerializeField] TMP_Text textoDialogoTelefono;
    private bool dialogoActivo;
    private int indexLinea;

    [SerializeField] public float tiempoEntreLetras = 0.2f;
    [SerializeField, TextArea(4, 6)] public string[] lineasDialogoTelefono; // Array de lineas de dialogo, cada elemento es una linea

    [Header("Variables activas por telefono")]
    [SerializeField] public GameObject[] objetosActivadosPorTelefono; // Array de objetos que se activan al interactuar con el telefono

    //asegurar que no lea dos veces el mismo dialogo
    public bool dialogoTelefonoLeido = false;
    

    private void Start()
    {
        // Desactivar el panel de diálogo y la imagen intermitente al iniciar
        panelDialogoTelefono.SetActive(false);
        imagenIntermitente.gameObject.SetActive(false);
    }

    public void Interactuar()
    {
        if (dialogoTelefonoLeido) return; // No hacer nada si el diálogo ya fue leído

        if (!dialogoActivo && !dialogoTelefonoLeido) // Si no hay dialogo activo y no se ha leido el dialogo
        {
            dialogoActivo = true; // Establecer dialogoActivo a true cuando se inicia el diálogo
            ActivarDialogo();
        }
        else if (dialogoActivo && textoDialogoTelefono.text == lineasDialogoTelefono[indexLinea]) // Si ya hay un dialogo activo
        {
            SiguienteLineaDialogo();
        }
        else
        {
            StopAllCoroutines();
            textoDialogoTelefono.text = lineasDialogoTelefono[indexLinea];
        }
    }

    private void ActivarDialogo() // Activa el dialogo, pone el tiempo en pausa y muestra las lineas de dialogo
    {
        panelDialogoTelefono.SetActive(true);
        imagenIntermitente.gameObject.SetActive(false); // Desactivar la imagen intermitente al iniciar el diálogo
        indexLinea = 0;
        Time.timeScale = 0f;
        StartCoroutine(MostrarLineasDialogo());
    }


    private void SiguienteLineaDialogo() // Muestra la siguiente linea de dialogo
    {
        indexLinea++;
        if (indexLinea < lineasDialogoTelefono.Length)
        {
            StopAllCoroutines();
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
        panelDialogoTelefono.SetActive(false);
        dialogoTelefonoLeido = true;
        //marcaOpcionInteraccion.SetActive(true);
        dialogoActivo = false;

        //Activar todos los objetos que nosotros queremos que se activen al interactuar con el telefono
        ActivarObjetosActivadosPorTelefono();
        Time.timeScale = 1f;
        imagenIntermitente.gameObject.SetActive(false); // Desactivar la imagen intermitente al finalizar el diálogo   
    }

    private IEnumerator MostrarLineasDialogo()
    {
        // Variable para controlar el tiempo de cambio de imagen
        float tiempoCambioImagen = Random.Range(0.2f, 1f);
        int indexImagen = 0;

        // Ciclo para mostrar las líneas de diálogo letra por letra
        foreach (char letra in lineasDialogoTelefono[indexLinea].ToCharArray())
        {
            // Mostrar la letra actual
            textoDialogoTelefono.text += letra;
            tiempoCambioImagen -= tiempoEntreLetras;

            // Si es hora de cambiar de imagen
            if (tiempoCambioImagen <= 0)
            {
                // Cambiar a la siguiente imagen de diálogo
                imagenesDialogoObjeto[indexImagen].gameObject.SetActive(false);
                indexImagen = (indexImagen + 1) % imagenesDialogoObjeto.Length;
                imagenesDialogoObjeto[indexImagen].gameObject.SetActive(true);

                tiempoCambioImagen = Random.Range(0.2f, 1f);
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

    public void ActivarObjetosActivadosPorTelefono() // Activa los objetos que se activan al interactuar con el telefono
    {
        foreach (var objeto in objetosActivadosPorTelefono)
        {
            objeto.SetActive(true);
        }
    }

}
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Telefono : MonoBehaviour, IInteractuable
{
    [Header("Elementos del dialogo")]
    public Collider2D colliderTelefono;

    [SerializeField] public GameObject marcaOpcionInteraccion;
    [SerializeField] GameObject panelDialogoTelefono;
    [SerializeField] public Image[] imagenesDialogoObjeto;
    [SerializeField] public Image imagenIntermitente;

    [SerializeField] TMP_Text textoDialogoTelefono;
    private bool dialogoActivo;
    private int indexLinea;

    [SerializeField] public float tiempoEntreLetras = 0.2f;

    [SerializeField, TextArea(4, 6)]
    public string[] lineasDialogoTelefono; // Array de lineas de dialogo, cada elemento es una linea

    [Header("Variables activas por telefono")]
    [SerializeField]
    public GameObject[] objetosActivadosPorTelefono; // Array de objetos que se activan al interactuar con el telefono

    //asegurar que no lea dos veces el mismo dialogo
    public bool dialogoTelefonoLeido = false;

    [SerializeField] AudioSource audioSource;


    private void Start()
    {
        // Desactivar el panel de di logo y la imagen intermitente al iniciar
        colliderTelefono = GetComponent<Collider2D>();
        panelDialogoTelefono.SetActive(false);
        imagenIntermitente.gameObject.SetActive(false);
        audioSource.Play();
    }

    public void Interactuar()
    {
        // dialogoTelefonoLeido = Dialogo.Instance.Empezar(panelDialogoTelefono.GetComponent<Image>(), lineasDialogoTelefono, tiempoEntreLetras, imagenesDialogoObjeto, dialogoTelefonoLeido, objetosActivadosPorTelefono, true); Fue una buena idea hasta que me canse de intentarlo
        if (dialogoTelefonoLeido) return; // No hacer nada si el di logo ya fue le do

        if (!dialogoActivo && !dialogoTelefonoLeido) // Si no hay dialogo activo y no se ha leido el dialogo
        {
            dialogoActivo = true; // Establecer dialogoActivo a true cuando se inicia el di logo
            ActivarDialogo();
        }
        else if (dialogoActivo &&
                 textoDialogoTelefono.text == lineasDialogoTelefono[indexLinea]) // Si ya hay un dialogo activo
        {
            SiguienteLineaDialogo();
        }
        else
        {
            StopAllCoroutines();
            textoDialogoTelefono.text = lineasDialogoTelefono[indexLinea];
            imagenesDialogoObjeto[0].gameObject.SetActive(true);
        }
    }

    private void ActivarDialogo() // Activa el dialogo, pone el tiempo en pausa y muestra las lineas de dialogo
    {
        panelDialogoTelefono.SetActive(true);
        imagenIntermitente.gameObject.SetActive(false); // Desactivar la imagen intermitente al iniciar el di logo
        indexLinea = 0;
        Time.timeScale = 0f;
        StartCoroutine(MostrarLineasDialogo());
        audioSource.Stop();
    }


    private void SiguienteLineaDialogo() // Muestra la siguiente linea de dialogo
    {
        textoDialogoTelefono.text = ""; // Coco es un juego de palabras (Lo hace ver mas pro los dialogos)
        indexLinea++;
        if (indexLinea < lineasDialogoTelefono.Length)
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
        panelDialogoTelefono.SetActive(false);
        dialogoTelefonoLeido = true;
        //marcaOpcionInteraccion.SetActive(true);
        dialogoActivo = false;

        //Activar todos los objetos que nosotros queremos que se activen al interactuar con el telefono
        ActivarObjetosActivadosPorTelefono();
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
        foreach (char letra in lineasDialogoTelefono[indexLinea])
        {
            // Mostrar la letra actual
            textoDialogoTelefono.text += letra;
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

    public void ActivarObjetosActivadosPorTelefono() // Activa los objetos que se activan al interactuar con el telefono
    {
        foreach (var objeto in objetosActivadosPorTelefono)
        {
            objeto.SetActive(true);
        }
    }
}
using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class Telefono : MonoBehaviour
{
    [SerializeField] public Collider2D colliderTelefono;
    [SerializeField] public GameObject marcaOpcionInteraccion;
    [SerializeField] GameObject panelDialogoTelefono;
    [SerializeField] TMP_Text textoDialogoTelefono;

    [SerializeField, TextArea(4, 6)] public string[] lineasDialogoTelefono; // Array de lineas de dialogo, cada elemento es una linea

    private float tiempoEntreLetras = 0.05f;
    private bool jugadorCerca = false;
    private bool dialogoActivo;
    private int indexLinea;

    public bool dialogoTelefonoLeido = false;

    public static Telefono instance { get; private set; }
    private void Awake()    // Singleton, evitar duplicados
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Si ya hay una instancia, destruye esta para mantener solo una
        }
    }

    void Update()
    {
        if(jugadorCerca && Input.GetButtonDown("Fire1")) // Si el jugador esta cerca y presiona el boton de interaccion
        {
            if (!dialogoActivo)
            {
                ActivarDialogo();
                
            } else if(textoDialogoTelefono.text == lineasDialogoTelefono[indexLinea])
            {
                SiguienteLineaDialogo();
            }
            else // Si el dialogo esta en proceso de mostrarse, se muestra completo
            {
                StopAllCoroutines();
                textoDialogoTelefono.text = lineasDialogoTelefono[indexLinea];
            }
        }
    }

    private void ActivarDialogo() // Activa el dialogo, pone el tiempo en pausa y muestra las lineas de dialogo
    {
        dialogoActivo = true;
        panelDialogoTelefono.SetActive(true);
        marcaOpcionInteraccion.SetActive(false);
        indexLinea = 0;
        Time.timeScale = 0f;
        StartCoroutine(MostrarLineasDialogo());

    }
    private void SiguienteLineaDialogo() // Muestra la siguiente linea de dialogo
    {
        indexLinea++;
        if (indexLinea < lineasDialogoTelefono.Length)
        {
            StartCoroutine(MostrarLineasDialogo());
        }
        else // Si ya no hay mas lineas de dialogo, se desactiva el panel de dialogo y se reactiva la opcion de interaccion
        {
            panelDialogoTelefono.SetActive(false);
            dialogoTelefonoLeido = true;
            marcaOpcionInteraccion.SetActive(true);
            dialogoActivo = false;
            Time.timeScale = 1f;
        }
    }
    private IEnumerator MostrarLineasDialogo() // Muestra las lineas de dialogo letra por letra usando corutinas
    {
        textoDialogoTelefono.text = string.Empty;
        foreach (char letra in lineasDialogoTelefono[indexLinea].ToCharArray())
        {
            textoDialogoTelefono.text += letra;
            yield return new WaitForSecondsRealtime(tiempoEntreLetras); // Espera un tiempo antes de mostrar la siguiente letra
        }
    }

    private void OnTriggerEnter2D(Collider2D colliderTelefono) // Si el jugador esta cerca del telefono, se activa la opcion de interaccion
    {
        if (colliderTelefono.gameObject.CompareTag("Player"))
        {
            jugadorCerca = true;
            marcaOpcionInteraccion.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D colliderTelefono) // Si el jugador se aleja del telefono, se desactiva la opcion de interaccion
    {
        if (colliderTelefono.gameObject.CompareTag("Player"))
        {
            jugadorCerca = false;
            marcaOpcionInteraccion.SetActive(false);
        }
    }
}

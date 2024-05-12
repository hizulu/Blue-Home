using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class Telefono : MonoBehaviour, IInteractuable
{
    [Header("Elementos del dialogo")]
    [SerializeField] public Collider2D colliderTelefono;
    [SerializeField] public GameObject marcaOpcionInteraccion;
    [SerializeField] GameObject panelDialogoTelefono;
    [SerializeField] TMP_Text textoDialogoTelefono;
    private bool dialogoActivo;
    private int indexLinea;

    [SerializeField] public float tiempoEntreLetras = 0.2f;
    [SerializeField, TextArea(4, 6)] public string[] lineasDialogoTelefono; // Array de lineas de dialogo, cada elemento es una linea

    [Header("Variables activas por telefono")]
    [SerializeField] public GameObject[] objetosActivadosPorTelefono; // Array de objetos que se activan al interactuar con el telefono

    //asegurar que no lea dos veces el mismo dialogo
    public bool dialogoTelefonoLeido = false;
    public void Interactuar()
    {
        if (!dialogoActivo && !dialogoTelefonoLeido) // Si no hay dialogo activo y no se ha leido el dialogo
        {
            ActivarDialogo();
        }
        else if (dialogoActivo) // Si ya hay un dialogo activo
        {
            SiguienteLineaDialogo();
        }
    }
    private void ActivarDialogo() // Activa el dialogo, pone el tiempo en pausa y muestra las lineas de dialogo
    {
        dialogoActivo = true;
        panelDialogoTelefono.SetActive(true);
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
            StartCoroutine(MostrarLineasDialogo());
        }
        else // Si ya no hay mas lineas de dialogo, se desactiva el panel de dialogo y se reactiva la opcion de interaccion
        {
            panelDialogoTelefono.SetActive(false);
            dialogoTelefonoLeido = true;
            marcaOpcionInteraccion.SetActive(true);
            dialogoActivo = false;
            //Activar todos los objetos que nosotros queremos que se activen al interactuar con el telefono
            ActivarObjetosActivadosPorTelefono();
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
    public void ActivarObjetosActivadosPorTelefono() // Activa los objetos que se activan al interactuar con el telefono
    {
        foreach (var objeto in objetosActivadosPorTelefono)
        {
            objeto.SetActive(true);
        }
    }
}
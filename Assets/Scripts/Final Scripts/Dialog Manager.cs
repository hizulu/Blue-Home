using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private List<GameObject> panelesSombra;
    [SerializeField] private GameObject panelTrasSombra;
    [SerializeField] private TMP_Text textosDialogoSombra;

    [SerializeField] private List<GameObject> panelesRuddy;
    [SerializeField] private GameObject panelTrasRuddy;
    [SerializeField] private TMP_Text textosDialogoRuddy;

    [SerializeField, TextArea] private string texto;
    [SerializeField] private List<string> textos;
    [SerializeField] public GameObject marcaOpcionInteraccion;

    // GameObjects adicionales
    [SerializeField] private GameObject objeto1;
    [SerializeField] private GameObject objeto2;
    [SerializeField] private GameObject objeto3;

    private int index;
    private int indexHablar = 1;

    public float tiempoEntreLetras = 0.2f;
    private bool dialogoActivo = false;
    private bool lineaCompleta = false;
    private bool dialogoIniciado = false; // Nueva bandera para controlar el inicio del dialogo

    private void Start()
    {
        index = 0; // Inicializar el indice
        DesactivarTodosLosPaneles(); // Desactivar todos los paneles al inicio
        DesactivarObjetosAdicionales(); // Desactivar objetos adicionales al inicio
    }

    private void Update()
    {
        if (!dialogoIniciado) return; // No hacer nada hasta que el dialogo se inicie

        if (Input.GetKeyDown(KeyCode.E) && !dialogoActivo && !lineaCompleta)
        {
            dialogoActivo = true;
            StartCoroutine(MostrarLineasDialogo());
        }

        // Permitir avanzar al siguiente dialogo despues de que se muestre completamente
        if (Input.GetKeyDown(KeyCode.E) && lineaCompleta)
        {
            lineaCompleta = false;
            dialogoActivo = true;
            indexHablar = 1 - indexHablar; // Alternar entre 0 y 1
            StartCoroutine(MostrarLineasDialogo());
        }
    }

    private void DesactivarPaneles(List<GameObject> paneles)
    {
        foreach (var panel in paneles)
        {
            panel.SetActive(false);
        }
    }

    private void DesactivarTodosLosPaneles()
    {
        DesactivarPaneles(panelesSombra);
        DesactivarPaneles(panelesRuddy);
        panelTrasSombra.SetActive(false);
        panelTrasRuddy.SetActive(false);
    }

    private void DesactivarObjetosAdicionales()
    {
        objeto1.SetActive(false);
        objeto2.SetActive(false);
        objeto3.SetActive(false);
    }

    private void ActivarPanelProgresivo(List<GameObject> paneles)
    {
        paneles[index % paneles.Count].SetActive(true);
    }

    private void ActivarPanelAleatorio(List<GameObject> paneles)
    {
        int randomIndex = Random.Range(0, paneles.Count);
        paneles[randomIndex].SetActive(true);
    }

    private void ActivarPanelProgresivoPorGrupos(List<GameObject> paneles, int grupo)
    {
        int grupoIndex = (index / 8) % paneles.Count;
        paneles[grupoIndex].SetActive(true);
    }

    private void GestionarObjetosAdicionales()
    {
        if (index == 14)
        {
            objeto1.SetActive(true);
            objeto2.SetActive(true);
        }
        else if (index == 18)
        {
            objeto2.SetActive(false);
            objeto3.SetActive(true);
        }
    }

    private IEnumerator MostrarLineasDialogo()
    {
        TMP_Text textoDialogoActual = indexHablar == 1 ? textosDialogoSombra : textosDialogoRuddy;
        GameObject panelTrasActual = indexHablar == 1 ? panelTrasSombra : panelTrasRuddy;
        List<GameObject> panelesActuales = indexHablar == 1 ? panelesSombra : panelesRuddy;
        List<GameObject> panelesOpuestos = indexHablar == 1 ? panelesRuddy : panelesSombra;
        GameObject panelTrasOpuesto = indexHablar == 1 ? panelTrasRuddy : panelTrasSombra;

        DesactivarPaneles(panelesOpuestos);
        panelTrasOpuesto.SetActive(false);

        DesactivarPaneles(panelesActuales);
        panelTrasActual.SetActive(true);
        if (indexHablar == 1)
        {
            ActivarPanelProgresivoPorGrupos(panelesActuales, index / 8);
        }
        else
        {
            ActivarPanelProgresivo(panelesActuales);
        }

        textoDialogoActual.text = ""; // Resetea el texto antes de comenzar

        foreach (char letra in textos[index])
        {
            textoDialogoActual.text += letra;
            yield return new WaitForSecondsRealtime(tiempoEntreLetras);
        }

        lineaCompleta = true;
        dialogoActivo = false;

        // Esperar entrada del jugador para continuar
        while (!Input.GetKeyDown(KeyCode.E))
        {
            yield return null;
        }

        index = (index + 1);

        GestionarObjetosAdicionales(); // Gestionar la activación de objetos adicionales

        if (index >= textos.Count)
        {
            // Diálogo completado
            DesactivarTodosLosPaneles();
            DesactivarObjetosAdicionales(); // Desactivar objetos adicionales al final
            dialogoIniciado = false;
            index = 0; // Reiniciar indice para posibles futuros dialogos

            GameManager.instance.CargarNivel(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void IniciarDialogo()
    {
        StartCoroutine(IniciarDialogoDespuesDeSegundos(2f));
    }

    private IEnumerator IniciarDialogoDespuesDeSegundos(float segundos)
    {
        yield return new WaitForSeconds(segundos);

        if (!dialogoActivo)
        {
            Debug.Log("Iniciando diálogo después de " + segundos + " segundos");
            dialogoIniciado = true; // Establecer la bandera de dialogo iniciado
            dialogoActivo = true;
            StartCoroutine(MostrarLineasDialogo());
        }
    }
}
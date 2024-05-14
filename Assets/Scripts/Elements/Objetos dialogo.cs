using System.Collections;
using UnityEngine;
using TMPro;

public class Objetosdialogo : MonoBehaviour, IInteractuable
{
    [SerializeField] public Collider2D colliderObjeto;
    [SerializeField] public GameObject marcaOpcionInteraccion;
    [SerializeField] GameObject panelDialogoObjeto;
    [SerializeField] TMP_Text textoDialogoObjeto;
    [SerializeField, TextArea(4,6)] public string[] lineasDialogoObjeto;

    public float tiempoEntreLetras = 0.2f;
    private bool dialogoActivo;
    private int indexLinea;

    public void Interactuar()
    {
        if (!dialogoActivo)
        {
            ActivarDialogo();
        }
        else if(textoDialogoObjeto.text == lineasDialogoObjeto[indexLinea])
        {
            DesactivarDialogo();
        }
        else
        {
            StopAllCoroutines();
            textoDialogoObjeto.text = lineasDialogoObjeto[indexLinea];
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
}

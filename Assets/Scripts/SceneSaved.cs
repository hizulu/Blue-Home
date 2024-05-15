using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSaved : MonoBehaviour
{
    public int sceneIndex=0;
    public bool crearNuevaPartida=false;
    private int defaultSceneIndex = 1;
    //private int defaultSceneIndex = 2;

    public static SceneSaved instance { get; private set; }
    void Start()
    {
        // Solo guarda la escena si no estamos en la escena del menú
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            GuardarEscena();
        }
    }

    public void GuardarEscena()
    {
        // Guarda el índice de la escena actual si no es la escena del menú
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedScene", sceneIndex);
    }

    public void CargarEscena()
    {
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            // Carga la escena guardada
            sceneIndex = PlayerPrefs.GetInt("SavedScene");
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            // Si no hay una escena guardada, carga la escena predeterminada
            SceneManager.LoadScene(defaultSceneIndex);
        }
    }

    public void CrearNuevaPartida()
    {
        // Borra la clave de la escena guardada y carga la escena predeterminada
        PlayerPrefs.DeleteKey("SavedScene");
        SceneManager.LoadScene(defaultSceneIndex);
        crearNuevaPartida = true;
        DatosGuardados.instance.BorrarDatos();
    }
}

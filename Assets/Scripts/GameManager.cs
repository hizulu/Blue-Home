using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public int sceneIndex = 0;
    public bool crearNuevaPartida = false;
    private int defaultSceneIndex = 1;
    //private int defaultSceneIndex = 2;
    int totalEscenas;

    public static GameManager instance { get; private set; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        totalEscenas = SceneManager.sceneCountInBuildSettings;
    }
    void Update()
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
        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        if (escenaActual != totalEscenas - 1)
        {
            CargarNivel.NivelCarga(escenaActual + 1);
            if (PlayerPrefs.HasKey("SavedScene"))
            {
                // Carga la escena guardada
                crearNuevaPartida = false;
                sceneIndex = PlayerPrefs.GetInt("SavedScene");
                SceneManager.LoadScene(sceneIndex);
            }
            else
            {
                // Si no hay una escena guardada, carga la escena predeterminada
                CrearNuevaPartida();
                crearNuevaPartida = false;
            }
        }        
    }

    public void CrearNuevaPartida()
    {
        // Borra la clave de la escena guardada y carga la escena predeterminada
        PlayerPrefs.DeleteKey("SavedScene");
        crearNuevaPartida = true;
        SceneManager.LoadScene(defaultSceneIndex);
    }
}

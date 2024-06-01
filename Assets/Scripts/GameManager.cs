using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public int sceneIndex = 0;
    public bool crearNuevaPartida = false;
    private int escenaPartidaNueva = 1;
    int totalEscenas;
    public static GameManager instance { get; private set; }

    //Para la partida guardada
    TipoGuardado partidaGuardada = null;
    string partidaPath;
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
        partidaPath = Application.persistentDataPath + "/partida.json";
    }
    private void Start()
    {
        totalEscenas = SceneManager.sceneCountInBuildSettings;
        CargarPartida();
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
        // Guarda el indice de la escena actual si no es la escena del menu
        sceneIndex = SceneManager.GetActiveScene().buildIndex;

        partidaGuardada = new TipoGuardado (scene:sceneIndex);

        JsonUtils.SaveToJson(partidaGuardada, partidaPath);
        // PlayerPrefs.SetInt("SavedScene", sceneIndex);
    }

    public void CargarEscena()
    {
        if(partidaGuardada != null) SceneManager.LoadScene(partidaGuardada.scene);   
    }
    void CargarPartida()
    {
        partidaGuardada = JsonUtils.LoadFromJson<TipoGuardado>(partidaPath);
    }

    public void CrearNuevaPartida()
    {
        // Borra la clave de la escena guardada y carga la escena predeterminada
        crearNuevaPartida = true;
        SceneManager.LoadScene(escenaPartidaNueva);
    }

}

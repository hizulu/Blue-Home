using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSaved : MonoBehaviour
{
    public int sceneIndex;
    public bool crearNuevaPartida=false;
    private int defaultSceneIndex = 1;
    //private int defaultSceneIndex = 2;

    public static SceneSaved instance { get; private set; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        GuardarEscena();
    }
    public void GuardarEscena() //Se guarda la escena actual en playerprefs
    {
        if(sceneIndex != 0)
        {
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("SavedScene", sceneIndex);
        }        
    }
    public void CargarEscena() //Se carga la escena guardada en playerprefs
    {
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            sceneIndex = PlayerPrefs.GetInt("SavedScene");
            SceneManager.LoadScene(sceneIndex);
        }
        else 
        { 
            SceneManager.LoadScene(defaultSceneIndex); 
        }
    }
    public void CrearNuevaPartida() //Se borra la escena guardada y se carga la escena por defecto
    {
        crearNuevaPartida = true;
        PlayerPrefs.DeleteKey("SavedScene");
        CargarEscena();
    }

}

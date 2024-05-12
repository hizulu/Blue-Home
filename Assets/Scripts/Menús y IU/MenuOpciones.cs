using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;


public class MenuOpciones : MonoBehaviour
{
    [Header("Controles de UI")]
    [SerializeField] Slider volumenGeneralSlider;
    [SerializeField] Slider volumenMusicaSlider;
    [SerializeField] Slider volumenRuidoSlider;
    [SerializeField] Slider brilloSlider;
    [SerializeField] Toggle modoVentanaToggle;
    [SerializeField] Light2D luz;

    [Header("Control de Musica")]
    [SerializeField] AudioMixer mixer;
    const string VOLUMEN_GENERAL = "Master";
    const string VOLUMEN_MUSICA = "Musica";
    const string VOLUMEN_RUIDO = "Ruido";

    public static MenuOpciones instance { get; private set; }
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
    public void Start() 
    {       
        
        volumenGeneralSlider.onValueChanged.AddListener(ActualizarVolumenGeneral);
        volumenMusicaSlider.onValueChanged.AddListener(ActualizarVolumenMusica);
        volumenRuidoSlider.onValueChanged.AddListener(ActualizarVolumenRuido);
        
        brilloSlider.onValueChanged.AddListener(ActualizarBrillo);
        modoVentanaToggle.onValueChanged.AddListener(ActualizarModoVentana);
    }
    public void MostrarMenuOpciones()
    {
        transform.parent.gameObject.SetActive(true); // Activa el GameObject padre en la jerarqu�a
        gameObject.SetActive(true);
    }
    //Volumenes de los audios del juego
    public void ActualizarVolumenGeneral(float value)
    {
        mixer.SetFloat(VOLUMEN_GENERAL, value);
    }
    public void ActualizarVolumenMusica(float value)
    {
        mixer.SetFloat(VOLUMEN_MUSICA, value);
    }
    public void ActualizarVolumenRuido(float value)
    {
        mixer.SetFloat(VOLUMEN_RUIDO, value);
    }
    
    public void ActualizarBrillo(float value)
    {
        luz.intensity = value;//Cambia la intensidad de la luz global usando el slider
    }


    public void ActualizarModoVentana(bool value)
    {
        Screen.fullScreen = !value; //Si el toggle est� desactivado, hay pantalla completa
        Debug.Log("Modo ventana: " + value);
    }
    public void Volver()
    {
        //Desactiva el men� de opciones para que es vea el men� principal
        transform.parent.gameObject.SetActive(false); 
        gameObject.SetActive(false);
        int escenaAnterior = PlayerPrefs.GetInt("SavedScene"); //Busca la escena anterior en la memoria
        Debug.Log("Escena anterior guardada: " + escenaAnterior);
        SceneManager.LoadScene(escenaAnterior); //Vuelve a la escena anterior
    }
}

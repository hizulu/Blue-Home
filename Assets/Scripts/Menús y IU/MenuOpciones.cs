using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class MenuOpciones : MonoBehaviour
{
    [Header("Controles de UI")]
    [SerializeField] Slider volumenGeneralSlider;
    [SerializeField] Slider volumenMusicaSlider;
    [SerializeField] Slider volumenRuidoSlider;
    [SerializeField] Slider brilloSlider;
    [SerializeField] Toggle modoVentanaToggle;
    [SerializeField] Image nivelBrilloPanel;

    //TODO: mirar por qué no va del todo

    [Header("Control de Musica")]
    [SerializeField] AudioMixer mixer;
    const string VOLUMEN_GENERAL = "MasterVolume";
    const string VOLUMEN_MUSICA = "MusicaVolume";
    const string VOLUMEN_RUIDO = "RuidoVolume";

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
        //volumenRuidoSlider.onValueChanged.AddListener(ActualizarVolumenRuido);
        brilloSlider.onValueChanged.AddListener(ActualizarBrillo);
        modoVentanaToggle.onValueChanged.AddListener(ActualizarModoVentana);
    }
    public void MostrarMenuOpciones()
    {
        transform.parent.gameObject.SetActive(true); // Activa el GameObject padre en la jerarquía
        gameObject.SetActive(true);
    }

    public void ActualizarVolumenGeneral(float value)
    {
        mixer.SetFloat(VOLUMEN_GENERAL, Mathf.Log10(value) * 20);
    }
    public void ActualizarVolumenMusica(float value)
    {
        mixer.SetFloat(VOLUMEN_MUSICA, Mathf.Log10(value)*20);
    }
    public void ActualizarVolumenRuido(float value)
    {
        mixer.SetFloat(VOLUMEN_RUIDO, Mathf.Log10(value) * 20);
    }


    public void ActualizarBrillo(float value)
    {
        nivelBrilloPanel.color = new Color(0, 0, 0, value / 255f);
    }


    public void ActualizarModoVentana(bool value)
    {
        Screen.fullScreen = !value;
    }
    public void Volver()
    {
        transform.parent.gameObject.SetActive(false);
        gameObject.SetActive(false);
        int escenaAnterior = PlayerPrefs.GetInt("escenaAnterior", 0);
        Debug.Log("Escena anterior guardada: " + escenaAnterior);
        SceneManager.LoadScene(escenaAnterior);
    }
}

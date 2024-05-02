using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuOpciones : MonoBehaviour
{
    [SerializeField] Slider volumenGeneralSlider;
    [SerializeField] Slider volumenMusicaSlider;
    [SerializeField] Slider brilloSlider;
    [SerializeField] Toggle modoVentanaToggle;
    [SerializeField] Image nivelBrilloPanel;

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
        AudioListener.volume = value;
    }


    public void ActualizarVolumenMusica(float value)
    {
        // Aquí puedes ajustar el volumen de la música según sea necesario
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

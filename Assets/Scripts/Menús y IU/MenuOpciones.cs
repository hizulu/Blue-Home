using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuOpciones : MonoBehaviour
{
    [SerializeField] Slider volumenGeneralSlider;
    [SerializeField] Slider volumenMusicaSlider;
    [SerializeField] Slider brilloSlider;
    [SerializeField] Toggle modoVentanaToggle;
    [SerializeField] Image nivelBrilloPanel;

    void Start()
    {
        // Suscribirse a los eventos de cambio de los sliders y el toggle
        volumenGeneralSlider.onValueChanged.AddListener(ActualizarVolumenGeneral);
        volumenMusicaSlider.onValueChanged.AddListener(ActualizarVolumenMusica);
        brilloSlider.onValueChanged.AddListener(ActualizarBrillo);
        modoVentanaToggle.onValueChanged.AddListener(ActualizarModoVentana);
    }

  
    void ActualizarVolumenGeneral(float value)
    {
        AudioListener.volume = value;
    }

   
    void ActualizarVolumenMusica(float value)
    {
        // Aquí puedes ajustar el volumen de la música según sea necesario
    }

   
    void ActualizarBrillo(float value)
    {
        nivelBrilloPanel.color = new Color(0, 0, 0, value);
    }

   
    void ActualizarModoVentana(bool value)
    {
        Screen.fullScreen = value;
    }
}

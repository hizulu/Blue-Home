using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DatosGuardados : MonoBehaviour
{
    [SerializeField] private Slider volumenGeneralSlider;
    [SerializeField] private Slider volumenMusicaSlider;
    [SerializeField] private Slider volumenRuidoSlider;
    [SerializeField] private Slider brilloSlider;
    [SerializeField] private Toggle modoVentanaToggle;

    [SerializeField] private float defaultVolumenGeneral=50f;
    [SerializeField] private float defaultVolumenMusica=50f;
    [SerializeField] private float defaultVolumenRuido = 50f;
    [SerializeField] private float defaultBrillo = 0.4f;
    [SerializeField] private bool defaultModoVentana=false;

    public static DatosGuardados instance { get; private set; }
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
        if (SceneSaved.instance.crearNuevaPartida==true)
        {
            BorrarDatos();
            SceneSaved.instance.crearNuevaPartida= false;
        }
        else
        {
            SceneSaved.instance.CargarEscena();
        }
        //Se cargan las opciones guardadas
        CargarOpciones();
    }

    public void CargarOpciones()
    {
        /*        
        //Se asignan los valores guardados en playerprefs a los sliders y el toggle llamando a la clave asociada
        volumenGeneralSlider.value = PlayerPrefs.GetFloat("VolumenGeneral");
        volumenMusicaSlider.value = PlayerPrefs.GetFloat("VolumenMusica");
        volumenRuidoSlider.value = PlayerPrefs.GetFloat("VolumenRuido");
        brilloSlider.value = PlayerPrefs.GetFloat("Brillo");
        if (PlayerPrefs.GetInt("ModoVentana") == 1)
        {
            modoVentanaToggle.isOn = true;
        }
        else
        {
            modoVentanaToggle.isOn = false;
        }*/

        //En caso de que no haya valores guardados en playerprefs, se asignan los valores por defecto
        if (!PlayerPrefs.HasKey("VolumenGeneral"))
        {
            PlayerPrefs.SetFloat("VolumenGeneral", defaultVolumenGeneral);
        }
        else
        {
            volumenGeneralSlider.value = PlayerPrefs.GetFloat("VolumenGeneral");
        }


        if (!PlayerPrefs.HasKey("VolumenMusica"))
        {
            PlayerPrefs.SetFloat("VolumenMusica", defaultVolumenMusica);
        }
        else
        {
            volumenMusicaSlider.value = PlayerPrefs.GetFloat("VolumenMusica");
        }


        if (!PlayerPrefs.HasKey("VolumenRuido"))
        {
            PlayerPrefs.SetFloat("VolumenRuido", defaultVolumenRuido);
        }
        else
        {
            volumenRuidoSlider.value = PlayerPrefs.GetFloat("VolumenRuido");
        }


        if (!PlayerPrefs.HasKey("Brillo"))
        {
            PlayerPrefs.SetFloat("Brillo", defaultBrillo);
        }
        else
        {
            brilloSlider.value = PlayerPrefs.GetFloat("Brillo");
        }


        if (!PlayerPrefs.HasKey("ModoVentana"))
        {
            PlayerPrefs.SetInt("ModoVentana", defaultModoVentana ? 1 : 0);
        }
        else
        {
            modoVentanaToggle.isOn = PlayerPrefs.GetInt("ModoVentana") == 1;
        }

    }
    //Playerprefs guarda los datos del valor de los sliders y el toggle junto con una clave asociada

    public void SetVolumenGeneralPrefs()
    {
        PlayerPrefs.SetFloat("VolumenGeneral", volumenGeneralSlider.value);
    }
    public void SetVolumenMusicaPrefs()
    {
        PlayerPrefs.SetFloat("VolumenMusica", volumenMusicaSlider.value);
    }
    public void SetVolumenRuidoPrefs()
    {
        PlayerPrefs.SetFloat("VolumenRuido", volumenRuidoSlider.value);
    }
    public void SetBrilloPrefs()
    {
        PlayerPrefs.SetFloat("Brillo", brilloSlider.value);
    }
    public void SetModoVentanaPrefs()
    {
        if (modoVentanaToggle.isOn)
        {
            PlayerPrefs.SetInt("ModoVentana", 1);
        }
        else
        {
            PlayerPrefs.SetInt("ModoVentana", 0);
        }
    }
    public void BorrarDatos()
    {
        PlayerPrefs.DeleteAll();
    }
}

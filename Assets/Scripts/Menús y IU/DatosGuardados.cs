using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DatosGuardados : MonoBehaviour
{
    [SerializeField] private Slider volumenGeneralSlider;
    [SerializeField] private Slider volumenMusicaSlider;
    [SerializeField] private Slider volumenRuidoSlider;
    [SerializeField] private Slider brilloSlider;
    [SerializeField] private Toggle modoVentanaToggle;

    [SerializeField] private float defaultVolumenGeneral = 50f;
    [SerializeField] private float defaultVolumenMusica = 50f;
    [SerializeField] private float defaultVolumenRuido = 50f;
    [SerializeField] private float defaultBrillo = 0.4f;
    [SerializeField] private bool defaultModoVentana = false;

    private string filePath;
    public static DatosGuardados instance { get; private set; }

    private const string MODO_VENTANA_PREF_KEY = "DatosGuardados.modoVentana";

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

        // Define la ruta del archivo de guardado
        filePath = Application.persistentDataPath + "/datos.json";
        CargarOpciones();
    }

    void Start()
    {
        // Carga las opciones guardadas
        AplicarModoVentana();
    }

    public void CargarOpciones()
    {
        // Comprueba si existe un archivo de guardado
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            OpcionesGuardadas opcionesGuardadas = JsonUtility.FromJson<OpcionesGuardadas>(dataAsJson);

            // Asigna los valores guardados a los sliders y el toggle
            volumenGeneralSlider.value = opcionesGuardadas.volumenGeneral;
            volumenMusicaSlider.value = opcionesGuardadas.volumenMusica;
            volumenRuidoSlider.value = opcionesGuardadas.volumenRuido;
            brilloSlider.value = opcionesGuardadas.brillo;
            modoVentanaToggle.isOn = opcionesGuardadas.modoVentana;
        }
        else
        {
            // Asigna los valores por defecto
            volumenGeneralSlider.value = defaultVolumenGeneral;
            volumenMusicaSlider.value = defaultVolumenMusica;
            volumenRuidoSlider.value = defaultVolumenRuido;
            brilloSlider.value = defaultBrillo;
            modoVentanaToggle.isOn = defaultModoVentana;
        }

        AplicarModoVentana();
    }

    public void GuardarOpciones()
    {
        //guardamos los datos 
        OpcionesGuardadas opcionesGuardadas = new OpcionesGuardadas
        {
            volumenGeneral = volumenGeneralSlider.value,
            volumenMusica = volumenMusicaSlider.value,
            volumenRuido = volumenRuidoSlider.value,
            brillo = brilloSlider.value,
            modoVentana = modoVentanaToggle.isOn
        };

        // Convierte el objeto OpcionesGuardadas a JSON
        string dataAsJson = JsonUtility.ToJson(opcionesGuardadas);

        // Escribe el JSON en el archivo de guardado
        File.WriteAllText(filePath, dataAsJson);

        // Guardar el estado del modo ventana
        GuardarModoVentana(modoVentanaToggle.isOn);
    }

    private void GuardarModoVentana(bool modoVentana)
    {
        PlayerPrefs.SetInt(MODO_VENTANA_PREF_KEY, modoVentana ? 1 : 0);
        //esque no se por que no se guarda bien jaja
        PlayerPrefs.Save();
    }

    private void AplicarModoVentana()
    {
        if (PlayerPrefs.HasKey(MODO_VENTANA_PREF_KEY))
        {
            bool modoVentana = PlayerPrefs.GetInt(MODO_VENTANA_PREF_KEY) == 1;
            modoVentanaToggle.isOn = modoVentana;
            Screen.fullScreen = !modoVentana;
        }
    }

    public void BorrarDatos()
    {
        // Elimina el archivo de guardado si existe, nos manejamos con un solo archivo de guardado
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}

[System.Serializable]
public class OpcionesGuardadas
{
    public float volumenGeneral;
    public float volumenMusica;
    public float volumenRuido;
    public float brillo;
    public bool modoVentana;
}

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
    }
    void Start()
    {
        // Carga las opciones guardadas
        CargarOpciones();
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

[System.Serializable] public class OpcionesGuardadas
{
    public float volumenGeneral;
    public float volumenMusica;
    public float volumenRuido;
    public float brillo;
    public bool modoVentana;
}


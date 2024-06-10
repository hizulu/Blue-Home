using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using System.IO;

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

    private string filePath;
    private const string MODO_VENTANA_PREF_KEY = "DatosGuardados.modoVentana"; // Usamos la misma clave que en DatosGuardados

    private void Start()
    {
        CargarOpciones();
        transform.parent.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        // Ruta de guardado
        filePath = Application.persistentDataPath + "/datos.json";
    }

    public void MostrarMenuOpciones()
    {
        transform.parent.gameObject.SetActive(true); // Activa el GameObject padre en la jerarquía
        gameObject.SetActive(true);
    }

    // Volumenes de los audios del juego
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
        luz.intensity = value; // Cambia la intensidad de la luz global usando el slider
    }

    public void ActualizarModoVentana(bool modoVentanaActivo)
    {
        Screen.fullScreen = modoVentanaActivo;
        GuardarOpciones(modoVentanaActivo);
    }

    private void GuardarOpciones(bool modoVentanaActivo)
    {
        // Guardamos todas las opciones
        OpcionesGuardadas opcionesGuardadas = new OpcionesGuardadas
        {
            volumenGeneral = volumenGeneralSlider.value,
            volumenMusica = volumenMusicaSlider.value,
            volumenRuido = volumenRuidoSlider.value,
            brillo = brilloSlider.value,
            modoVentana = modoVentanaActivo
        };

        // Convierte el objeto OpcionesGuardadas a JSON
        string dataAsJson = JsonUtility.ToJson(opcionesGuardadas);

        // Escribe el JSON en el archivo de guardado
        File.WriteAllText(filePath, dataAsJson);

        // Guardar el estado del modo ventana en PlayerPrefs
        PlayerPrefs.SetInt(MODO_VENTANA_PREF_KEY, modoVentanaActivo ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void CargarOpciones()
    {
        // Cargamos todas las opciones
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            OpcionesGuardadas opcionesGuardadas = JsonUtility.FromJson<OpcionesGuardadas>(dataAsJson);

            // Asignamos los valores guardados a los sliders y el toggle
            volumenGeneralSlider.value = opcionesGuardadas.volumenGeneral;
            volumenMusicaSlider.value = opcionesGuardadas.volumenMusica;
            volumenRuidoSlider.value = opcionesGuardadas.volumenRuido;
            brilloSlider.value = opcionesGuardadas.brillo;
            modoVentanaToggle.isOn = opcionesGuardadas.modoVentana;

            // Aplicamos el modo ventana
            Screen.fullScreen = opcionesGuardadas.modoVentana;
        }
    }

    public void Volver()
    {
        // Cargar la escena anterior
        int index = PlayerPrefs.GetInt("escenaAnterior");
        SceneManager.LoadScene(index);
    }
}

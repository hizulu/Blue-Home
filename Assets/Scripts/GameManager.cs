using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int sceneIndex = 0;
    public bool crearNuevaPartida = false;
    private int escenaPartidaNueva = 1;
    int totalEscenas;
    public static GameManager instance { get; private set; }
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
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            GuardarEscena();
        }
    }

    public void GuardarEscena()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        partidaGuardada = new TipoGuardado(scene: sceneIndex);
        JsonUtils.SaveToJson(partidaGuardada, partidaPath);
    }

    public void CargarEscena()
    {
        if (partidaGuardada != null) CargarNivel.NivelCarga(partidaGuardada.scene); // Cargar la siguiente escena con la pantalla de carga
    }

    void CargarPartida()
    {
        partidaGuardada = JsonUtils.LoadFromJson<TipoGuardado>(partidaPath);
    }

    public void CrearNuevaPartida()
    {
        if (crearNuevaPartida)
        {
            PlayerPrefs.DeleteAll();
            //SceneManager.LoadScene(escenaPartidaNueva);
            CargarNivel.NivelCarga(escenaPartidaNueva); // Cargar la escena de la partida nueva con la pantalla de carga

        }
    }
}
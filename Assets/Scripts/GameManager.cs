using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int sceneIndex = 0;
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
        if (SceneManager.GetActiveScene().buildIndex != 0)
            GuardarEscena();
    }

    public void GuardarEscena()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        partidaGuardada = new TipoGuardado(scene: sceneIndex);
        JsonUtils.SaveToJson(partidaGuardada, partidaPath);
    }

    public void CargarEscena()
    {
        if (partidaGuardada != null)
            CargarNivel(partidaGuardada.scene);
        Debug.Log("Cargando escena guardada");
    }

    void CargarPartida()
    {
        partidaGuardada = JsonUtils.LoadFromJson<TipoGuardado>(partidaPath);
    }

    public void CrearNuevaPartida()
    {
        PlayerPrefs.DeleteAll();
        CargarNivel(escenaPartidaNueva);
    }

    public void CargarNivel(int nivelACargar)
    {
        StartCoroutine(IniciarCarga(nivelACargar));
    }

    IEnumerator IniciarCarga(int nivel)
    {
        Debug.Log("Iniciando carga de la escena " + nivel);
        // Iniciar la carga asincrona de la escena principal
        AsyncOperation cargaAsincrona = SceneManager.LoadSceneAsync(nivel);

        // Cargar la escena de "carga falsa" de forma aditiva
        SceneManager.LoadScene(13, LoadSceneMode.Additive);

        // Evitar la activacion automotica de la escena
        cargaAsincrona.allowSceneActivation = false;

        // Esperar a que la carga alcance el 90% antes de activar la escena
        while (!cargaAsincrona.isDone)
        {
            Debug.Log($"Progreso de carga: {cargaAsincrona.progress * 100}%");

            // Si la carga ha alcanzado el 90%, permitir la activacion de la escena
            if (cargaAsincrona.progress >= 0.9f)
            {
                Debug.Log("Carga completada al 90%, activando escena...");
                cargaAsincrona.allowSceneActivation = true;
            }

            yield return null;
        }

        yield return new WaitForSeconds(1);

        // Descargar la escena de "carga falsa"
        SceneManager.UnloadSceneAsync(13);

        Debug.Log($"Escena {nivel} cargada con exito.");
    }
}

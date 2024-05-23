using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CinematicaManager : MonoBehaviour
{
    [Header("Elementos de la cinematica")]
    public GameObject botonPlay;
    public GameObject botonPause;
    public GameObject botonskip;
    public VideoPlayer videoPlayer;
    //public AudioSource audioSource;   //TODO: Implementar audio por slider en la cinematica

    [Header("Variables de la cinematica")]
    public bool isPlaying;
    public bool isPaused;

    private Coroutine hideButtonsCoroutine;

    private void Start()
    {
        // Obtener el VideoPlayer adjunto si no se ha asignado en el Inspector
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // Verificar si el VideoPlayer está correctamente adjunto
        if (videoPlayer == null)
        {
            Debug.LogError("No hay un VideoPlayer adjunto al objeto CinematicaManager.");
            return;
        }

        // Inicializar botones como invisibles
        botonPlay.SetActive(false);
        botonPause.SetActive(false);
        botonskip.SetActive(false);

        // Registrar el evento para detectar el final del video
        videoPlayer.loopPointReached += OnVideoEnd;

        // Comenzar la reproducción automáticamente
        videoPlayer.Play();
        isPlaying = true;
        isPaused = false;

        // Ocultar el cursor al inicio
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (isPlaying)
            {
                PausarVideo();
            }
            else if (isPaused)
            {
                ReanudarVideo();
            }
        }
    }

    public void ReanudarVideo()
    {
        if (videoPlayer == null) return;

        videoPlayer.Play();
        isPlaying = true;
        isPaused = false;
        botonPlay.SetActive(false);
        botonPause.SetActive(true);
        botonskip.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Iniciar corrutina para ocultar botones después de 3 segundos
        if (hideButtonsCoroutine != null)
        {
            StopCoroutine(hideButtonsCoroutine);
        }
        hideButtonsCoroutine = StartCoroutine(DesaparecerBotones());
    }

    public void PausarVideo()
    {
        if (videoPlayer == null) return;

        videoPlayer.Pause();
        isPaused = true;
        isPlaying = false;
        botonPlay.SetActive(true);
        botonPause.SetActive(false);
        botonskip.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Cancelar la corutina que oculta los botones si el video está pausado
        if (hideButtonsCoroutine != null)
        {
            StopCoroutine(hideButtonsCoroutine);
        }
    }

    public void SkipVideo()
    {
        // Cargar la siguiente escena inmediatamente
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator DesaparecerBotones()
    {
        yield return new WaitForSeconds(3);
        if (isPlaying)
        {
            botonPlay.SetActive(false);
            botonPause.SetActive(false);
            botonskip.SetActive(false);
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // Cargar la siguiente escena cuando el video termine
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

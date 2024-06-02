using UnityEngine;
using UnityEngine.Video;

public class VideoInicial : MonoBehaviour
{
    [Header("Elementos de la cinematica")]
    public VideoPlayer videoPlayer;

    //quiero que el video se reproduzca en bucle de fondo
    private void Start()
    {
        // Obtener el VideoPlayer adjunto si no se ha asignado en el Inspector
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // Verificar si el VideoPlayer est� correctamente adjunto
        if (videoPlayer == null)
        {
            Debug.LogError("No hay un VideoPlayer adjunto al objeto VideoInicial.");
            return;
        }

        // Comenzar la reproducci�n autom�ticamente
        videoPlayer.isLooping = true;
        videoPlayer.Play();
    }
}
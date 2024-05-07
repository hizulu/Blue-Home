using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class uiSombra : MonoBehaviour
{
    //La idea es que sombraController llame a este script para que se active el UI de la sombra
    //Este script se encargará de activar el video y que desaparezca cuando termine

    [SerializeField] GameObject reloj;
    public VideoPlayer videoPlayer;
    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void OnVideoFinished(VideoPlayer player)
    {
        gameObject.SetActive(false);
        reloj.SetActive(true);
    }
    public void PlayVideo()
    {
        gameObject.SetActive(true);
        reloj.SetActive(false);
        videoPlayer.Play(); // Inicia la reproducción del video
        Debug.Log ("Reproduciendo video");
    }

    public void StopVideo()
    {
        videoPlayer.Stop(); // Detiene la reproducción del video
        gameObject.SetActive(true);
    }

}

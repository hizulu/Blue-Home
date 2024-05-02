using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class uiSombra : MonoBehaviour
{
    //La idea es que sombraController llame a este script para que se active el UI de la sombra
    //Este script se encargará de activar el video y que desaparezca cuando termine

    public VideoPlayer videoPlayer;
    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer player)
    {
        gameObject.SetActive(false); 
    }
    public void PlayVideo()
    {
        gameObject.SetActive(true);
        videoPlayer.Play(); // Inicia la reproducción del video
        Debug.Log ("Reproduciendo video");
    }

    public void StopVideo()
    {
        videoPlayer.Stop(); // Detiene la reproducción del video
        gameObject.SetActive(true);
    }
}

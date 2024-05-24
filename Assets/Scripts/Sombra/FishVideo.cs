using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FishVideo : MonoBehaviour
{
    //La idea es que sombraController llame a este script para que se active el UI de la sombra
    //Este script se encargar� de activar el video y que desaparezca cuando termine

    [SerializeField] GameObject[] desactivarObjetos;
    public VideoPlayer videoPlayer;
    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void OnVideoFinished(VideoPlayer player)
    {
        gameObject.SetActive(false);
        foreach (GameObject objeto in desactivarObjetos)
        {
            objeto.SetActive(true);
        }
        Time.timeScale = 1;
    }
    public void PlayVideo()
    {
        gameObject.SetActive(true);
        foreach (GameObject objeto in desactivarObjetos)
        {
            objeto.SetActive(false);
        }
        videoPlayer.Play(); // Inicia la reproducci�n del video
        Debug.Log ("Reproduciendo video");
        Time.timeScale = 0;
    }

    public void StopVideo()
    {
        videoPlayer.Stop(); // Detiene la reproducci�n del video
        gameObject.SetActive(true);
    }

}

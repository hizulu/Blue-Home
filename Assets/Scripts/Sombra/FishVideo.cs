using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FishVideo : MonoBehaviour
{
    [SerializeField] GameObject[] desactivarObjetos;
    public VideoPlayer videoPlayer;

    // Evitar que se activen ciertos objetos al terminar el video
    private List<bool> estadosOriginales;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer = GetComponent<VideoPlayer>();
        estadosOriginales = new List<bool>(desactivarObjetos.Length);
    }

    void OnVideoFinished(VideoPlayer player)
    {
        gameObject.SetActive(false);
        for (int i = 0; i < desactivarObjetos.Length; i++)
        {
            desactivarObjetos[i].SetActive(estadosOriginales[i]);
        }
        Time.timeScale = 1;
    }

    public void PlayVideo()
    {
        gameObject.SetActive(true);
        estadosOriginales.Clear(); // Limpia la lista antes de usarla
        foreach (GameObject objeto in desactivarObjetos)
        {
            estadosOriginales.Add(objeto.activeSelf);
            objeto.SetActive(false);
        }

        videoPlayer.Play(); 
        Debug.Log("Reproduciendo video");
        Time.timeScale = 0;
    }   
    public void StopVideo()
    {
        videoPlayer.Stop(); 
        gameObject.SetActive(true);
    }
}

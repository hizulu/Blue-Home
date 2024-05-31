using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ActiveVideoAdquirido : MonoBehaviour
{
    [SerializeField] private GameObject ImagenVideoAdquirido;
    public VideoPlayer videoPlayer;
    [SerializeField] GameObject[] desactivarObjetos;

    // Eliminar el audio de fondo
    public AudioSource musicaFondo;
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

        // Reanuda la m�sica de fondo
        musicaFondo.UnPause();
    }
    public void PlayVideo()
    {
        gameObject.SetActive(true);
        videoPlayer.Play(); // Inicia la reproducci�n del video
        Debug.Log("Reproduciendo video");
        foreach (GameObject objeto in desactivarObjetos)
        {
            objeto.SetActive(false);
        }
        Time.timeScale = 0;

        // Pausa la m�sica de fondo
        musicaFondo.Pause();
    }
    public void StopVideo()
    {
        videoPlayer.Stop(); // Detiene la reproducci�n del video
        gameObject.SetActive(true);

        // Reanuda la m�sica de fondo
        musicaFondo.UnPause();
    }
}
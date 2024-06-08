using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    //manejar la musica del juego
    public static MusicManager instance;
    public AudioSource audioSource;
    [SerializeField] public AudioClip[] musica;
    [SerializeField] private float fadeTimeMusica = 1f;

    //la musica que se esta reproduciendo
    private AudioClip musicaActual;

    //diferentes salidas de audio para la musica
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    //musica
    public IEnumerator PlayMusic(int index)
    {
        if (audioSource.isPlaying)
        {
            yield return StartCoroutine(FadeOut(fadeTimeMusica));
        }
        musicaActual = musica[index];
        audioSource.clip = musicaActual;
        audioSource.Play();
        StartCoroutine(FadeIn(fadeTimeMusica));
    }
    //Estos son para fade out y fade in de sonido
    IEnumerator FadeOut(float fadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    IEnumerator FadeIn(float fadeTime)
    {
        float startVolume = audioSource.volume;
        audioSource.volume = 0;
        audioSource.Play();

        while (audioSource.volume < startVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        audioSource.volume = startVolume;
    }

    //parar ruido y sonido de forma manual
    public void StopMusic()
    {
        audioSource.Stop();
    }
    public void StartMusic()
    {
        audioSource.Play();
    }
}

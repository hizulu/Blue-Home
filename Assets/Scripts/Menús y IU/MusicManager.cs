using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    //manejar la musica del juego
    public static MusicManager instance;
    public AudioSource audioSource;
    [SerializeField] public AudioClip[] musica;
    [SerializeField] public AudioClip[] ruido;
    [SerializeField] private float fadeTimeMusica = 1f;
    [SerializeField] private float fadeTimeRuido = 0f;

    //la musica que se esta reproduciendo
    private AudioClip musicaActual;
    private AudioClip ruidoActual;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
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
    //ruido
    public IEnumerator PlayRuido(int index)
    {
        if (audioSource.isPlaying)
        {
            yield return StartCoroutine(FadeOut(fadeTimeRuido));
        }
        ruidoActual = ruido[index];
        audioSource.clip = ruidoActual;
        audioSource.Play();
        StartCoroutine(FadeIn(fadeTimeRuido));
    }
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

}

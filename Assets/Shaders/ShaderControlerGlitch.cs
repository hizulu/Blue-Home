using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using URPGlitch.Runtime.AnalogGlitch;
using URPGlitch.Runtime.DigitalGlitch;

public class ShaderControlerGlitch : MonoBehaviour
{
    [Header("Elementos")]
    [SerializeField] private GameObject post;
    [SerializeField]private GameObject enemigo;
    [SerializeField]private GameObject sombra;
    private Volume volume;

    [Header("Variables de efectos de glitch")]
    [SerializeField]private float maxIntensity = 0.05f; // Valor maximo de intensidad para los efectos de glitch
    [SerializeField] private float maxDistance = 5f; // Distancia maxima a la que los efectos tienen su máxima intensidad

    [Header("Sonido")]
    public AudioSource audioSourcePersecucion;     

    private AnalogGlitchVolume analogGlitch;
    private DigitalGlitchVolume digitalGlitch;

    private void Start()
    {
        volume = post.GetComponent<Volume>();

        volume.profile.TryGet(out analogGlitch);
        analogGlitch.scanLineJitter.value = 0f;
        analogGlitch.verticalJump.value = 0f;
        analogGlitch.horizontalShake.value = 0f;
        analogGlitch.colorDrift.value = 0f;

        volume.profile.TryGet(out digitalGlitch);
        digitalGlitch.intensity.value = 0f;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, enemigo.transform.position);

        if (sombra.active || enemigo.active == false || distance > maxDistance)
        {
            analogGlitch.scanLineJitter.value = 0f;
            analogGlitch.verticalJump.value = 0f;
            analogGlitch.horizontalShake.value = 0f;
            analogGlitch.colorDrift.value = 0f;
            digitalGlitch.intensity.value = 0f;
            if (audioSourcePersecucion.isPlaying)
            {
                audioSourcePersecucion.loop = false;
                audioSourcePersecucion.Stop();
            }
        }
        else
        {
            float intensity = Mathf.Clamp01(1 - (distance / maxDistance)) * maxIntensity;
            analogGlitch.scanLineJitter.value = intensity;
            analogGlitch.verticalJump.value = intensity;
            analogGlitch.horizontalShake.value = intensity;
            analogGlitch.colorDrift.value = intensity;
            digitalGlitch.intensity.value = intensity*5;

            audioSourcePersecucion.volume = intensity;
            if (!audioSourcePersecucion.isPlaying)
            {
                audioSourcePersecucion.loop = true;
                audioSourcePersecucion.Play();
            }
        } 
    }
}

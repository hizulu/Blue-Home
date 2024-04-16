using UnityEngine;
using System;
using TMPro;

public class Reloj : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoReloj;

    private int horas = 6;
    private int minutos = 0;
    private bool colorCambiado = false;

    private float tiempoTranscurrido = 0f;
    private float velocidadDelTiempo = 16f / 6f;

    void Update()
    {
        tiempoTranscurrido += velocidadDelTiempo * Time.deltaTime;
        ActualizarTiempo();
        MostrarTiempo();
        if (colorCambiado)
            cambioApariencia();
    }

    void ActualizarTiempo()
    {
        horas = Mathf.FloorToInt(tiempoTranscurrido / 60) % 24;
        minutos = Mathf.FloorToInt(tiempoTranscurrido % 60);
        horas = Mathf.Clamp(horas, 6, 22);
        if (horas == 22 && minutos == 0 && !colorCambiado)
        {
            velocidadDelTiempo = 0f;
            colorCambiado = true;
        }
    }

    void MostrarTiempo()
    {
        string horaFormateada = horas.ToString("00") + ":" + minutos.ToString("00");
        textoReloj.text = horaFormateada;
    }

    void cambioApariencia()
    {
        textoReloj.color = Color.red;
        textoReloj.transform.localScale = new Vector3(1, 1, 1);
        float vibracion = Mathf.PingPong(Time.time * 5, 0.3f) + 1;
        textoReloj.transform.localScale = new Vector3(vibracion, vibracion, vibracion);
    }
}

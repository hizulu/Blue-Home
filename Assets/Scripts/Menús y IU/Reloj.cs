using UnityEngine;
using System;
using TMPro;

public class Reloj : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoReloj;

    public float horas;
    private float minutos;
    private bool colorCambiado = false;

    public float tiempoTranscurrido { get; private set; }
    public float velocidadDelTiempo = 3.2f; //5 minutos en la vida real
    //private float velocidadDelTiempo = 1440f;

    public static Reloj instance { get; private set; }
    void Update()
    {
        tiempoTranscurrido += velocidadDelTiempo *Time.deltaTime;
        ActualizarTiempo();
        MostrarTiempo();
        if (colorCambiado)
            cambioApariencia();
    }

    void ActualizarTiempo()
    {
        horas = (Mathf.FloorToInt(tiempoTranscurrido / 60) % 24)+6; 
        minutos = Mathf.FloorToInt(tiempoTranscurrido % 60);
        horas = Mathf.Clamp(horas, 6, 22); // Limitar el tiempo de 6:00 a 22:00

        if (horas == 22 && minutos == 0 && !colorCambiado) // Cambiar el color del reloj a las 22:00
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
        float vibracion = Mathf.PingPong(Time.time * 2, 0.3f) + 1; //UNDONE Estoy igual hay que mirarlo mas adelante
        textoReloj.transform.localScale = new Vector3(vibracion, vibracion, vibracion); //Hace que los números del reloj vibren
    }
    public void AdelantarTiempo(float horasAdelantar)
    {
        tiempoTranscurrido += horasAdelantar * 3600f; // Convertir horas a segundos
        ActualizarTiempo();
        MostrarTiempo();
    }

}

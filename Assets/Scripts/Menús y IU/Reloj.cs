using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class Reloj : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoReloj;

    public float horas;
    private float minutos;
    private bool colorCambiado = false;

    public float tiempoTranscurrido { get; private set; }
    public float velocidadDelTiempo = 3.2f; //5 minutos en la vida real

    [SerializeField] GameObject textoMision;
    [SerializeField] GameObject ImagenInventario;

    public static Reloj instance { get; private set; }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (!colorCambiado)
        {
            tiempoTranscurrido += velocidadDelTiempo * Time.deltaTime;
            ActualizarTiempo();
            MostrarTiempo();
        }
        else
        {
            cambioApariencia(); // Continuar cambiando la apariencia mientras colorCambiado es verdadero
        }
    }

    void ActualizarTiempo()
    {
        horas = (Mathf.FloorToInt(tiempoTranscurrido / 60) % 24) + 6;
        minutos = Mathf.FloorToInt(tiempoTranscurrido % 60);
        horas = Mathf.Clamp(horas, 6, 22); // Limitar el tiempo de 6:00 a 22:00

        if (horas >= 22 && minutos >= 0 && !colorCambiado) // Cambiar el color del reloj a las 22:00
        {
            velocidadDelTiempo = 0f;
            colorCambiado = true;
            Invoke("FinTiempo", 5f); // Invocar FinTiempo una vez después de 5 segundos
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
        float vibracion = Mathf.PingPong(Time.time * 2, 0.3f) + 1;
        textoReloj.transform.localScale = new Vector3(vibracion, vibracion, vibracion);
    }

    public void AdelantarTiempo(float horasAdelantar)
    {
        if (horas == 22 && minutos == 0) return; // No adelantar el tiempo si ya es 22:00
        else if (horas == 21 && minutos > 0) // Si es 21:00 y hay minutos, adelantar a 22:00
        {
            tiempoTranscurrido = 22 * 60f; // Convertir horas a segundos
            ActualizarTiempo();
            MostrarTiempo();
            return;
        }
        else
        {
            tiempoTranscurrido += horasAdelantar * 60f; // Convertir horas a segundos
            ActualizarTiempo();
            MostrarTiempo();
        }
    }

    void FinTiempo()
    {
        textoMision.SetActive(false);
        ImagenInventario.SetActive(false);
        GameManager.instance.CargarNivel(SceneManager.GetActiveScene().buildIndex);
    }
}
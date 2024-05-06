using UnityEngine.Tilemaps;
using UnityEngine;

public class VentanasTiempo : MonoBehaviour
{
    public AnimatedTile[] ventanasFrames; // Array para almacenar los frames de las ventanas
    private Reloj reloj;
    private Tilemap tilemap;

    private int frameActual; // Frame actual de la animacion
    private float tiempoDesdeUltimoAvance; // Tiempo transcurrido desde el ultimo avance de la animacion
    private float tiempoEntreAvances = 3f; // Tiempo entre cada avance de la animacion (en horas)

    private Vector3Int offset; // Offset para calcular la posición del tilemap en relación con la posición del objeto

    private void Awake()
    {
        reloj = FindObjectOfType<Reloj>();
        tilemap = GetComponent<Tilemap>();

        // Detener la animacion
        tilemap.animationFrameRate = 0f;
        tiempoDesdeUltimoAvance = 0f;

        // Inicializar frameActual en 0
        frameActual = 0;

        // Calcular el offset entre la posición del objeto y la posición de las celdas del tilemap
        offset = tilemap.WorldToCell(transform.position) - tilemap.cellBounds.min;

        // Establecer el primer frame de la animacion en la posición del tilemap
        tilemap.SetTile(tilemap.WorldToCell(transform.position), ventanasFrames[frameActual]);
    }

    private void Update()
    {
        if (reloj != null)
        {
            // Obtener la hora actual
            float tiempoTranscurrido = reloj.tiempoTranscurrido;
            CambiarColorVentanas(tiempoTranscurrido);
        }
    }

    private void CambiarColorVentanas(float tiempoTranscurrido)
    {
        // Calcular la hora actual en el juego
        float horas = (tiempoTranscurrido / 60f) % 24f;

        // Calcular el frame actual basado en la hora actual
        int numeroTotalFrames = ventanasFrames.Length;
        frameActual = Mathf.FloorToInt(horas / 24f * numeroTotalFrames) % numeroTotalFrames;

        // Obtener la posición actual del tilemap
        Vector3Int tilePosition = tilemap.WorldToCell(transform.position) - offset;

        // Establecer el frame actual en la posición del tilemap
        tilemap.SetTile(tilePosition, ventanasFrames[frameActual]);

        tiempoDesdeUltimoAvance += Time.deltaTime;

        if (tiempoDesdeUltimoAvance >= tiempoEntreAvances)
        {
            tiempoDesdeUltimoAvance = 0f;
            AvanzarAnimacion();
        }
    }

    private void AvanzarAnimacion()
    {
        // Avanzar al siguiente frame
        frameActual = (frameActual + 1) % ventanasFrames.Length;

        // Obtener la posición actual del tilemap
        Vector3Int tilePosition = tilemap.WorldToCell(transform.position) - offset;

        // Establecer el frame actual en la posición del tilemap
        tilemap.SetTile(tilePosition, ventanasFrames[frameActual]);
    }
}

using UnityEngine.Tilemaps;
using UnityEngine;

public class VentanasTiempo : MonoBehaviour
{
    public Tilemap tilemap;
    public AnimatedTile[] animatedTiles;
    private Reloj reloj;
    private int lastFrameIndex = 0;

    private void Start()
    {
        reloj = FindObjectOfType<Reloj>();
        UpdateAnimation();
    }

    private void Update()
    {
        if (reloj != null)
        {
            float horas = reloj.horas;
            int numeroTotalFrames = animatedTiles.Length;

            // Calcula el frame actual
            int frameIndex = Mathf.FloorToInt(horas * numeroTotalFrames / 16);

            // Diferencia horaria de 3 horas
            if (Mathf.Abs(frameIndex - lastFrameIndex) > 3)
            {
                UpdateAnimation();
            }
        }
    }

    private void UpdateAnimation()
    {
        float horas = reloj.horas;
        int numeroTotalFrames = animatedTiles.Length;

        // Calcula el frame actual
        int frameIndex = Mathf.FloorToInt(horas * numeroTotalFrames / 16);

        for (int i = 0; i < animatedTiles.Length; i++)
        {
            // Actualiza el sprite del primer frame de la animación al sprite correspondiente al nuevo frameIndex
            animatedTiles[i].m_AnimatedSprites[0] = animatedTiles[i].m_AnimatedSprites[frameIndex];
        }

        lastFrameIndex = frameIndex; // Actualiza el último índice de frame
        tilemap.RefreshAllTiles(); // Actualiza el tilemap
    }
}

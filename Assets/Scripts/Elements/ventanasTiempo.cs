using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class VentanasTiempo : MonoBehaviour
{
    public AnimatedTile ventanas;
    private Reloj reloj;
    private Tilemap tilemap;

    private void Awake()
    {
        reloj = FindObjectOfType<Reloj>();
        tilemap = GetComponent<Tilemap>();
    }

    private void CambiarColorVentanas(int nuevaHora)
    {
        int numeroTotalFrames = ventanas.m_AnimatedSprites.Length;

        if (numeroTotalFrames == 0)
        {
            Debug.LogError("No hay frames");
            return;
        }
        Vector3Int tilePosition = new Vector3Int(0, 0, 0);

        float porcentajeCompletado = nuevaHora / 24f; // progresion de 0 a 1
        //esto es para interpolar entre los frames
        int frameActual = Mathf.FloorToInt(Mathf.Lerp(0, numeroTotalFrames - 1, porcentajeCompletado));

        tilemap.SetTile(Vector3Int.zero, ventanas.GetTile(Vector3Int.zero).GetAnimatedTile(frameActual)); //TODO
    }
}

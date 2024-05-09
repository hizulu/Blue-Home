using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pared1 : MonoBehaviour
{
    private bool isPlayerInRange;
    [SerializeField] public float transitionSpeed = 10f;
    public float Transparencia = 0f;
    public Tilemap tilemap;
    public Transform player;

    //Versión sencilla para volver invisible las paredes en el área para atravesarlas
    public enum Modo
    {
        Show = 0,
        Transparent = 1,
        Nothing = -1,
    }
    public Modo modo;

    private void Start()
    {
        modo = Modo.Nothing;
        tilemap = GetComponent<Tilemap>();
    }
    void Update()
    {
        //invisibilizarlo en esas posiciones
        if (isPlayerInRange)
        {
            modo = Modo.Transparent;
        }
        else
        {
            modo = Modo.Show;
        }

        if (modo.Equals(Modo.Transparent))
        {
            if (Transparencia <= 0.3f)
            {
                modo = Modo.Nothing;
            }
            Transparencia -= transitionSpeed * Time.deltaTime;
            Transparencia = Mathf.Max(Transparencia, 0.3f);
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, Transparencia);
        }
        if (modo.Equals(Modo.Show))
        {
            if (Transparencia >= 1f)
            {
                modo = Modo.Nothing;
            }
            Transparencia += transitionSpeed * Time.deltaTime;
            Transparencia = Mathf.Min(Transparencia, 1f);
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, Transparencia);
        }

        //override a la capa del player con su sprite
        if (transform.position.y - 1 > player.position.y)
        {
            GetComponent<TilemapRenderer>().sortingOrder = 5;
        }
        else
        {
            GetComponent<TilemapRenderer>().sortingOrder = 3;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}

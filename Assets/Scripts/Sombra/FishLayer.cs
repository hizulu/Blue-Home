using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishLayer : MonoBehaviour
{
    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        positionLayer();
    }
    void Update()
    {
        positionLayer();
    }
    void positionLayer()
    {
        //override a la capa del player con su sprite
        if (transform.position.y - 1 > player.position.y)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 3;
        }
        else
        {
            GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
    }
}

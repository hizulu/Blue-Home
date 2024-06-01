using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterBasura : MonoBehaviour, IInteractuable
{
    [SerializeField] private GameObject imagenInteractuable;

    public void Interactuar()
    {
        imagenInteractuable.SetActive(false);
    }
}

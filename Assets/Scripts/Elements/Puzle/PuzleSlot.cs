using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzleSlot : MonoBehaviour
{
    [SerializeField] public SpriteRenderer Renderer;

    public void Colocado()
    {
        // Método para manejar cuando una pieza es colocada correctamente
        Debug.Log("Pieza colocada correctamente en el slot.");
    }
}


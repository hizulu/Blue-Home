using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiezaPuzle : MonoBehaviour
{
    private bool deslizando;
    public bool colocado;
    private Vector2 desplazamiento, posicionInicial;

    private PuzleSlot _slot;


    public static PiezaPuzle instance { get; private set; }

    public void Init(PuzleSlot slot)
    {
        _slot = slot; // Asigna el slot pasado como parámetro
    }

    private void Awake()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        if (colocado) return;
        if (!deslizando) return;

        var posicionMouse = GetPosicionRaton();
        transform.position = posicionMouse - desplazamiento;
    }

    private void OnMouseDown()
    {
        deslizando = true;
        desplazamiento = GetPosicionRaton() - (Vector2)transform.position;
    }

    private void OnMouseUp()
    {
        if (_slot == null)
        {
            Debug.LogError("El slot no está asignado");
            return;
        }

        if (Vector2.Distance(transform.position, _slot.transform.position) < 3)
        {
            transform.position = _slot.transform.position;
            _slot.Colocado();
            colocado = true;
            deslizando = false; // Deja de deslizar después de colocar
        }
        else
        {
            transform.position = posicionInicial;
            deslizando = false;
        }
    }

    Vector2 GetPosicionRaton()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}

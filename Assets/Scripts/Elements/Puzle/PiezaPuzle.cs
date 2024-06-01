using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiezaPuzle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Renderer;

    private bool deslizando, colocado;
    private Vector2 desplazamiento, posicionInicial;

    private PuzleSlot _slot;

    public void Init(PuzleSlot slot)
    {
        Renderer.sprite = slot.Renderer.sprite;
        _slot=slot;
    }

    private void Awake()
    {
        posicionInicial = transform.position;
    }
    void Update()
    {
        if(colocado) return;
        if(!deslizando) return;
      
        var posicionMouse = GetPosicionRaton();
        transform.position = posicionMouse- desplazamiento;
    }
    private void OnMouseDown()
    {
        deslizando = true;

        desplazamiento = GetPosicionRaton()-(Vector2)transform.position;
    }

    private void OnMouseUp()
    {
        if(Vector2.Distance(transform.position, _slot.transform.position) < 3)
        {
            transform.position = _slot.transform.position;
            _slot.Colocado();
            colocado = true;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    //Esto es para el cursor personalizado
    public Texture2D cursorNormal;
    public Texture2D cursorPulsado;

    public Vector2 hotSpotPersonalizado;
    public Vector2 hotSpotPersonalizadoPulsado;

    private void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.SetCursor(cursorNormal, hotSpotPersonalizado, CursorMode.Auto);
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Cursor.SetCursor(cursorNormal, hotSpotPersonalizado, CursorMode.Auto);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Cursor.SetCursor(cursorPulsado, hotSpotPersonalizadoPulsado, CursorMode.Auto);
        }
    }
    public void OnButtonCursorEnter()
    {
        Cursor.SetCursor(cursorPulsado, hotSpotPersonalizadoPulsado, CursorMode.Auto);
    }
    public void OnButtonCursorExit()
    {
        Cursor.SetCursor(cursorNormal, hotSpotPersonalizado, CursorMode.Auto);
    }
}

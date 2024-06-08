using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonesMenuPrincipal : MonoBehaviour
{
    [SerializeField] Button newGameButton;
    [SerializeField] Button loadGameButton;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        newGameButton.onClick.AddListener(GameManager.instance.CrearNuevaPartida);
        loadGameButton.onClick.AddListener(GameManager.instance.CargarEscena);
    }
}

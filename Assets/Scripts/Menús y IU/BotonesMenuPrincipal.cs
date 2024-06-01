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
        newGameButton.onClick.AddListener(GameManager.instance.CrearNuevaPartida);
        loadGameButton.onClick.AddListener(GameManager.instance.CargarEscena);
    }
}

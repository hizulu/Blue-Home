using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public void Jugar()
    {
        int EscenaActual=(SceneManager.GetActiveScene()).buildIndex;
        SceneManager.LoadScene(EscenaActual+1);
    }
    public void Salir()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }
}

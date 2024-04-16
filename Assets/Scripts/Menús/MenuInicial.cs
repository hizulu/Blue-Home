using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Jugar()
    {
        int EscenaActual=(SceneManager.GetActiveScene()).buildIndex;
        SceneManager.LoadScene(EscenaActual+1);
    }
    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
}

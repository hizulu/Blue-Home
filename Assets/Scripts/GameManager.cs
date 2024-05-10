using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CambioEscenaActiva())
        SceneSaved.instance.GuardarEscena();
    }

    private bool CambioEscenaActiva()
    {
        return SceneManager.GetActiveScene().buildIndex != SceneSaved.instance.sceneIndex;

    }
}

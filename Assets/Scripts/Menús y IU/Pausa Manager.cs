using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaManager : MonoBehaviour
{
    [SerializeField] GameObject menuPausa;
    [SerializeField] Reloj relojScript;
    [SerializeField] GameObject relojGameObject;
    [SerializeField] MenuOpciones menuOpciones;

    private void Awake()
    {
        menuPausa.SetActive(false);
    }

    void Start()
    {
        relojScript = relojGameObject.GetComponent<Reloj>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuPausa.activeSelf)
            {
                Continuar(); // Si el menu de pausa está activo, al presionar Escape, continuamos en lugar de pausar
            }
            else
            {
                Pausar(); // Si el menu de pausa no está activo, al presionar Escape, lo pausamos
            }
        }
    }

    public void Pausar()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0;
        relojScript.velocidadDelTiempo = 0f;
    }

    public void Continuar()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1;
        relojScript.velocidadDelTiempo = 12f;
    }

    public void Opciones()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("escenaAnterior", index);
        SceneManager.LoadScene(0);
        menuOpciones.MostrarMenuOpciones();

    }
    public void Salir()
    {
        SceneManager.LoadScene(0);
    }
}

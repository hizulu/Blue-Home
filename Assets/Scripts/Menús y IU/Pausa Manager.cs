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
        // Establecer el men� de pausa como inactivo al inicio
        menuPausa.SetActive(false);
    }

    void Start()
    {
        relojScript = relojGameObject.GetComponent<Reloj>();
    }

    private void Update()
    {
        // Manejar la pausa y continuidad al presionar Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuPausa.activeSelf)
            {
                Continuar(); // Si el men� de pausa est� activo, continuamos el juego
            }
            else
            {
                Pausar(); // Si el men� de pausa no est� activo, pausamos el juego
            }
        }
    }

    public void Pausar()
    {
        // Activar el men� de pausa y pausar el juego
        menuPausa.SetActive(true);
        Time.timeScale = 0;
        relojScript.velocidadDelTiempo = 0f;
    }

    public void Continuar()
    {
        // Desactivar el men� de pausa y continuar el juego
        menuPausa.SetActive(false);
        Time.timeScale = 1;
        relojScript.velocidadDelTiempo = 3.2f; // Ajusta la velocidad del tiempo seg�n sea necesario
    }

    public void Opciones()
    {
        // Guardar la escena actual y cargar la escena del men� de opciones
        int index = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("escenaAnterior", index);
        SceneManager.LoadScene(0);

        // Mostrar el men� de opciones
        menuOpciones.MostrarMenuOpciones();
    }

    public void Salir()
    {
        // Cargar la escena principal
        SceneManager.LoadScene(0);
    }
}

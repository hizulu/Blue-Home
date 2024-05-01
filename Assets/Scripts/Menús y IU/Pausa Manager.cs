using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaManager : MonoBehaviour
{
    [SerializeField] GameObject menuPausa;
    [SerializeField] Reloj relojScript;
    [SerializeField] GameObject relojGameObject;


    void Start()
    {
        relojScript = relojGameObject.GetComponent<Reloj>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pausar();
            Time.timeScale = (gameObject.activeSelf) ? 0 : 1;
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
        MenuOpciones.instance.gameObject.SetActive(true);

    }
    public void Salir()
    {
        SceneManager.LoadScene(0);
    }
}

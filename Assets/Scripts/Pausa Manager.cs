using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausaManager : MonoBehaviour
{
    [SerializeField] GameObject menuPausa;
    [SerializeField]  Reloj relojScript;
    void Start()
    {
        relojScript = GetComponent<Reloj>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = (gameObject.activeSelf) ? 0 : 1;
        }
    }
    public void Pausar()
    {
        menuPausa.SetActive(true);
        Time.timeScale=0;
        relojScript.velocidadDelTiempo = 0f;
    }
    public void Continuar()
    {
        menuPausa.SetActive(false);
        Time.timeScale=1;
        relojScript.velocidadDelTiempo = 12f;
    }
}

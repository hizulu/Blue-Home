using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    int nivelACargar;
    void Start()
    {
        nivelACargar = CargarNivel.siguienteNivel;
        StartCoroutine(IniciarCarga(nivelACargar));
    }

    IEnumerator IniciarCarga(int nivel)
    {
        yield return new WaitForSeconds(1f);
        AsyncOperation cargaAsincorna = SceneManager.LoadSceneAsync(nivel);

        cargaAsincorna.allowSceneActivation = false;

        while (!cargaAsincorna.isDone)
        {
            //float progreso = Mathf.Clamp01(cargaAsincorna.progress / 0.9f);
            if (cargaAsincorna.progress >= 0.9f)
            {
                cargaAsincorna.allowSceneActivation = true;
            }

            yield return null;
        }


    }
}

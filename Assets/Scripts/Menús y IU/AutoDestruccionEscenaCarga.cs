//destruyeme la escena a los 2 segundos de aparecer
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoDestruccionEscenaCarga : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Destruir());
    }

    IEnumerator Destruir()
    {
        yield return new WaitForSeconds(2);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }
}
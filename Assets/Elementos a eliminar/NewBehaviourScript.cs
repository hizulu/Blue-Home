using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] Button boton;


    public void Botton()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index+1);
        GameManager.instance.GuardarEscena();
        /*if (GameManager.instance.sceneIndex >1)
        {
            gameObject.SetActive(false);
        }*/
    }


}

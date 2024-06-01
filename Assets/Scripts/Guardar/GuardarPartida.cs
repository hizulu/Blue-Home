using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuardarPartida : MonoBehaviour
{
    void Start()
    {
        GameManager.instance.GuardarEscena();
    }

}

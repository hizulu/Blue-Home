using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArmarioJuguete : MonoBehaviour, IInteractuable
{
    void IInteractuable.Interactuar()
    {
        CargarPuzle();
    }
    void CargarPuzle()
    {
        GameManager.instance.CargarNivel(6);
    }
}

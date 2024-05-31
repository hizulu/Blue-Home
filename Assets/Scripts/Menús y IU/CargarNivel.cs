using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class CargarNivel
{
    public static int siguienteNivel;

    public static void NivelCarga(int indice)
    {
        siguienteNivel = indice;
        SceneManager.LoadScene("EscenaCarga");
    }
}

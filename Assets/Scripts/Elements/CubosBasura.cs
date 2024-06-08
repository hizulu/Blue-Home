using UnityEngine;
using UnityEngine.SceneManagement;

public class CubosBasura : MonoBehaviour, IInteractuable
{
    [SerializeField] private GameObject textoMision;
    [SerializeField] private GameObject ImagenInventario;
    static public int basuraTotal = 0;

    public void Interactuar()
    {
        if (!VariablesEstaticas.completadolv2 && VariablesEstaticas.inventarioLleno && VariablesEstaticas.CalcetinBasura == 1)
        {
            basuraTotal++;
            ImagenInventario.SetActive(false);
            VariablesEstaticas.CalcetinBasura = 0;
            VariablesEstaticas.inventarioLleno = false;
        }
        if (basuraTotal + Cestodelaropa.ropaTotal >= VariablesEstaticas.basuraTotal)
        {
            Debug.Log(("Cubo de basura lleno"));
            GameManager.instance.CargarNivel(SceneManager.GetActiveScene().buildIndex+1);
            VariablesEstaticas.completadolv2 = true;
            textoMision.SetActive(false);
            ImagenInventario.SetActive(false);
        }
        Debug.Log("Basura en cubo: " + basuraTotal + Cestodelaropa.ropaTotal);
    }
}
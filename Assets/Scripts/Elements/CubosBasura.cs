using UnityEngine;

public class CubosBasura : MonoBehaviour, IInteractuable
{
    [SerializeField] private GameObject textoMision;
    [SerializeField] private GameObject ImagenInventario;
    private int basuraEnCubo = 0;
    private int basuraTotal = 0;

    public void Interactuar()
    {
        if (!VariablesEstaticas.completadolv2 && VariablesEstaticas.inventarioLleno && VariablesEstaticas.CalcetinBasura == 1)
        {
            basuraEnCubo++;
            basuraTotal++;
            ImagenInventario.SetActive(false);
            VariablesEstaticas.CalcetinBasura = 0;
            VariablesEstaticas.inventarioLleno = false;

            if (basuraEnCubo >= 3)
            {
                Debug.Log("Cubo de basura lleno");
                ImagenInventario.SetActive(false);
            }
        }
        if (basuraTotal == VariablesEstaticas.basuraTotal)
        {
            VariablesEstaticas.completadolv2 = true;
            textoMision.SetActive(false);
            ImagenInventario.SetActive(false);
            GameManager.instance.CargarNivel(4);
        }
    }
}
using UnityEngine;

public class Cestodelaropa : MonoBehaviour, IInteractuable
{
    [SerializeField] private GameObject textoMision;
    [SerializeField] private GameObject ImagenInventario;
    private int ropaEnCubo = 0;
    private int ropaTotal = 0;

    public void Interactuar()
    {
        if (!VariablesEstaticas.completadolv2 && VariablesEstaticas.inventarioLleno && VariablesEstaticas.CalcetinBasura == -1)
        {
            ropaEnCubo++;
            ropaTotal++;
            ImagenInventario.SetActive(false);
            VariablesEstaticas.CalcetinBasura = 0;
            VariablesEstaticas.inventarioLleno = false;

            if (ropaEnCubo >= 3)
            {
                Debug.Log("Cubo de ropa lleno");
                ImagenInventario.SetActive(false);
            }
        }
        if (ropaTotal == VariablesEstaticas.basuraTotal)
        {
            VariablesEstaticas.completadolv2 = true;
            textoMision.SetActive(false);
            ImagenInventario.SetActive(false);
        }
    }
}

using UnityEngine;

public class Cestodelaropa : MonoBehaviour, IInteractuable
{
    [SerializeField] private GameObject textoMision;
    [SerializeField] private GameObject ImagenInventario;
    static public int ropaTotal = 0;

    public void Interactuar()
    {
        if (!VariablesEstaticas.completadolv2 && VariablesEstaticas.inventarioLleno && VariablesEstaticas.CalcetinBasura == -1)
        {
            ropaTotal++;
            ImagenInventario.SetActive(false);
            VariablesEstaticas.CalcetinBasura = 0;
            VariablesEstaticas.inventarioLleno = false;
        }
        if (ropaTotal + CubosBasura.basuraTotal >= VariablesEstaticas.basuraTotal)
        {
            Debug.Log(("Cubo de ropa lleno"));
            GameManager.instance.CargarNivel(4);
            VariablesEstaticas.completadolv2 = true;
            textoMision.SetActive(false);
            ImagenInventario.SetActive(false);

        }
        Debug.Log("Basura en cubo: " + ropaTotal + CubosBasura.basuraTotal);
    }
}

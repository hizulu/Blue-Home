using UnityEngine;

public class Television : MonoBehaviour, IInteractuable
{
    [SerializeField] private GameObject[] objetos;

    [Header("Sombra")]
    [SerializeField] private GameObject sombra;
    public float sombraDistancia = 5f;

    private void Start()
    {
        foreach (var objeto in objetos) //Lo inicializamos desactivado todo
        {
            objeto.SetActive(false);
        }
    }
    private void Update()
    {
        float distanciaSombra = Vector2.Distance(sombra.transform.position, transform.position);
        if (distanciaSombra < sombraDistancia)
        {
            objetos[1].SetActive(true);
        }
        else
        {
            objetos[1].SetActive(false);
        }
    }
    public void Interactuar()  
    {
        objetos[0].SetActive(!objetos[0].activeSelf);
    }
}
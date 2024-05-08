using UnityEngine;

public class Television : MonoBehaviour
{
    [SerializeField] private GameObject[] objetos;
    private BoxCollider2D boxCollider;

    [Header("Sombra")]
    [SerializeField] private GameObject sombra;
    public float sombraDistancia = 5f;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
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
    private void Interactuar()  //Esto siempre va a ser llamado desde el jugador
    {
        objetos[0].SetActive(!objetos[0].activeSelf);
    }
}
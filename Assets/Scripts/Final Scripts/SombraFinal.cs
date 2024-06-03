using System.Collections;
using UnityEngine;

public class SombraFinal : MonoBehaviour
{

    [SerializeField] private DialogManager dialogManager;

    private Animator anim;
    private Transform player;
    public float speed = 1f;
    public float distanceToMove = 2f;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //mueve de forma progresiva la sombra hacia el jugador
        StartCoroutine(MoverSombra());
    }
    IEnumerator MoverSombra()
    {
        // Hacer que la sombra se mueva hacia la izquierda
        anim.SetFloat("CaminaHorz", 1f);
        anim.SetFloat("CaminaVert", 0f);

        // Calcular la posicion final en el eje X
        float finalPositionX = player.position.x + distanceToMove;

        // En eje x
        while (Mathf.Abs(transform.position.x - finalPositionX) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(finalPositionX, transform.position.y), speed * Time.deltaTime);
            yield return null;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Toque Indebido con la sombra");
            dialogManager.IniciarDialogo();
        }
    }
}

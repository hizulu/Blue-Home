using System.Collections;
using UnityEngine;

public class RuddyFinal : MonoBehaviour
{
    private Animator _anim; 
    private void Start()
    {
        _anim = GetComponent<Animator>();
        StartCoroutine(MirarAbajoYDerecha());
    }

    private IEnumerator MirarAbajoYDerecha()
    {
        // Hacer que el personaje mire hacia abajo
        _anim.SetFloat("CaminaHorz", 0f);
        _anim.SetFloat("CaminaVert", -1f);

        // Esperar unos segundos para que se gire a la derecha (y mire a la sombra)
        yield return new WaitForSeconds(5f);

        // Hacer que el personaje mire a la derecha
        _anim.SetFloat("CaminaHorz", 1f);
        _anim.SetFloat("CaminaVert", 0f);
    }
}
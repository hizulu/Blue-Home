using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelarAccionesJugador : MonoBehaviour, IInteractuable
{
    [SerializeField] GameObject dialogo;
    [SerializeField] GameObject reloj;
    public void Interactuar()
    {
        dialogo.SetActive(false);
        if (CogerObjetos.instance.completado)// Si el jugador ha completado la tarea
        {
            Reloj.instance.velocidadDelTiempo = 0f;
            Invoke("DesactivarReloj", 30f); 
        } 
            
    }
    void DesactivarReloj()
    {
        reloj.SetActive(false);
    }
}

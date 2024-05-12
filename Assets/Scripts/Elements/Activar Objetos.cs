using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarObjetos : MonoBehaviour
{
    [SerializeField] public GameObject Reloj;
    [SerializeField] public GameObject Sombra;
    [SerializeField] public GameObject TriggerPlantas;
    [SerializeField] public GameObject TriggerPecera;
    [SerializeField] public GameObject TriggerFoto;

    void Update()
    {
       if(Telefono.instance.dialogoTelefonoLeido==true)
        {
            Reloj.SetActive(true);
            Sombra.SetActive(true);
            TriggerPlantas.SetActive(true);
            TriggerPecera.SetActive(true);
            TriggerFoto.SetActive(true);
        }
    }
}

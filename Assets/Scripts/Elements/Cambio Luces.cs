using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CambioLuces : MonoBehaviour
{
    [SerializeField] Light2D luzGlobal;
    [SerializeField] GameObject relojUI;

    void Start()
    {
        if(relojUI.activeSelf) // Si el reloj está activo
            StartCoroutine(ActualizarColorPeriodicamente());
        else
            luzGlobal.color = new Color(152f / 255f, 180f / 255f, 255f / 255f, 1f);
    }

    IEnumerator ActualizarColorPeriodicamente()
    {
        while (true)
        {
            CambioColor();
            yield return new WaitForSeconds(60f); // Actualizar cada minuto
        }
    }
    void CambioColor()
    {
        switch (Reloj.instance.horas)
        {
            case float horas when horas >= 6 && horas < 8:
                luzGlobal.color = new Color(152 / 255f, 204f / 255f, 255f / 255f, 1f);
                break;
            case float horas when horas >= 8 && horas < 10:
                luzGlobal.color = new Color(152f / 255f, 249f / 255f, 255f / 255f, 1f);
                break;
            case float horas when horas >= 10 && horas < 12:
                luzGlobal.color = new Color(152f / 255f, 152f / 255f, 179f / 255f, 1f);
                break;
            case float horas when horas >= 12 && horas < 14:
                luzGlobal.color = new Color(201f / 255f, 255f / 255f, 152f / 255f, 1f);
                break;
            case float horas when horas >= 14 && horas < 16:
                luzGlobal.color = new Color(255f / 255f, 241f / 255f, 152f / 255f, 1f);
                break;
            case float horas when horas >= 16 && horas < 18:
                luzGlobal.color = new Color(255f / 255f, 166f / 255f, 152f / 255f, 1f);
                break;
            case float horas when horas >= 18 && horas < 20:
                luzGlobal.color = new Color(255f / 255f, 152f / 255f, 203f / 255f, 1f);
                break;
            case float horas when horas >= 20 && horas < 22:
                luzGlobal.color = new Color(202f / 255f, 152f / 255f, 255f / 255f, 1f);
                break;
           default:
                luzGlobal.color = new Color(152f / 255f, 180f / 255f, 255f / 255f, 1f);
                break;
        }
    }
}

using System.Threading.Tasks;
using UnityEngine;

public class ManageVentanas : MonoBehaviour
{
    public GameObject[] ventanas;

    private bool[] cambiosHechos = new bool[4]; // Array para seguir el estado de los cambios
    async void Start() // async void es necesario para usar Task.Delay, que mejor que un start asyncrono xd
    {
        await comprobarVentanas();
    }
    async Task comprobarVentanas()
    {
        while (true)
        {
            int horaActual = (int)Reloj.instance.horas;

            switch (horaActual)
            {
                case 10:
                    if (!cambiosHechos[0])
                    {
                        cambiarVentanas(0);
                        cambiosHechos[0] = true;
                        ResetCambios(0);
                    }

                    break;
                case 14:
                    if (!cambiosHechos[1])
                    {
                        cambiarVentanas(1);
                        cambiosHechos[1] = true;
                        ResetCambios(1);
                    }

                    break;
                case 18:
                    if (!cambiosHechos[2])
                    {
                        cambiarVentanas(2);
                        cambiosHechos[2] = true;
                        ResetCambios(2);
                    }

                    break;
                case 22:
                    if (!cambiosHechos[3])
                    {
                        cambiarVentanas(3);
                        cambiosHechos[3] = true;
                        ResetCambios(3);
                    }

                    return;
            }

            await Task.Delay(20000); // Espera 20 segundos antes de volver a comprobar
        }
    }
    void cambiarVentanas(int index)
    {
        for (int i = 0; i < ventanas.Length; i++)
        {
            // Desactiva todos los GameObjects
            ventanas[i].SetActive(false);
        }

        // Activa el GameObject correspondiente a la hora actual
        if (index < ventanas.Length)
        {
            ventanas[index].SetActive(true);
        }
    }
    void ResetCambios(int indexExcept)
    {
        for (int i = 0; i < cambiosHechos.Length; i++)
        {
            if (i != indexExcept)
            {
                cambiosHechos[i] = false;
            }
        }
    }
}
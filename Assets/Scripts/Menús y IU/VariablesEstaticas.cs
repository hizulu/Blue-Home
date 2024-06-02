
using UnityEngine;

public class VariablesEstaticas
{
    //variables de control del primer nivel
    public static bool recogidolv1 = false;
    public static bool completadolv1 = false;


    //Esto es del segundo nivel
    public static bool completadolv2 = false;
    public static bool recogidolv2 = false;

    public static int basuraRecolectada = 0;
    public static bool inventarioLleno = false;
    public static int CalcetinBasura = 0; //Si es 1 es basura, si es -1 es calcetin
    public static int basuraTotal = 9; // La cantidad de basura necesaria para llenar un cubo

    //Esto es del cuarto nivel

    public static bool recogidolv4 = false;
    public static bool completadolvl4 = false;

}

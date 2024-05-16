using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Catalogo/Fase1")]

public class Test : ScriptableObject
{
    [SerializeField]
    public List<GameObject> gameObjects;
}

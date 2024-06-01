using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzleManager : MonoBehaviour
{
    [SerializeField] private List<PuzleSlot> slotPrefabs;
    [SerializeField] private PiezaPuzle piezasPrefabs;
    [SerializeField] private Transform piezasPadre, slotPadre;
    
    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        var LugaresRandom = slotPrefabs.OrderBy(s=>Random.value).Take(3).ToList();

        for(int i=0; i< LugaresRandom.Count; i++)
        {
            var spawnedSlot = Instantiate(LugaresRandom[i], slotPadre.GetChild(i).position, Quaternion.identity);

            var spawnedPieza = Instantiate(piezasPrefabs, piezasPadre.GetChild(i).position, Quaternion.identity);
            spawnedPieza.Init(spawnedSlot);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzleManager : MonoBehaviour
{
    [SerializeField] private List<PuzleSlot> slotPrefabs;  // Lista de slots del puzzle
    [SerializeField] private List<PiezaPuzle> piezasPrefabs;  // Lista de slots del puzzle
    [SerializeField] private PiezaPuzle piezaPrefab;       // Prefab base de las piezas del puzzle
    [SerializeField] private Transform piezasPadre;        // Padre de las piezas del puzzle
    [SerializeField] private List<Sprite> piezasSprites;   // Lista de sprites para las piezas del puzzle

    private List<PiezaPuzle> piezas = new List<PiezaPuzle>(); // Lista de piezas del puzzle

    [SerializeField] GameObject Puzle;
    [SerializeField] GameObject PuzleCompleto;
    void Start()
    {
        Spawn();
        Cursor.lockState = CursorLockMode.None; // Desbloquea el cursor
        Cursor.visible = true; // Hace el cursor visible
    }
    void Update()
    {
        PiezasColocadas();
    }

    void Spawn()
    {
        // Asegurarse de que haya suficientes sprites para los slots
        if (piezasSprites.Count < slotPrefabs.Count)
        {
            Debug.LogError("No hay suficientes sprites para los slots del puzzle.");
            return;
        }

        // Ordenar y tomar una cantidad de slots aleatoria
        var lugaresRandom = slotPrefabs.OrderBy(s => Random.value).Take(piezasSprites.Count).ToList();

        for (int i = 0; i < lugaresRandom.Count; i++)
        {
            // Instanciar las piezas del puzzle            
            var spawnedPieza = Instantiate(piezasPrefabs[i], piezasPadre.GetChild(i).position, Quaternion.identity, piezasPadre);
            spawnedPieza.Init(lugaresRandom[i]);
            piezas.Add(spawnedPieza); // Agrega la pieza a la lista de piezas
        }
    }

    void PiezasColocadas()
    {
        int piezasColocadas = 0;
        foreach (var pieza in piezas)
        {
            if (pieza.colocado)
            {
                piezasColocadas++;
            }
        }

        if (piezasColocadas == slotPrefabs.Count)
        {
            Puzle.SetActive(false);
            PuzleCompleto.SetActive(true);
            GameManager.instance.CargarNivel(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
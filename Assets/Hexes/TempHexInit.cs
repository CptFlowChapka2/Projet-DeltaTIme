using System;
using UnityEngine;

public class TempHexInit : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private GameObject hexPrefab;
    [SerializeField] private int numberOfRows;
    private Hex hex;

    private int iteration = 0;
    
    private void Start()
    {
        hex = GetComponent<Hex>();
        if (numberOfRows == 13)
        {
            gridManager.hexes[0, 0] = hex;
        }
        else
        {
            gridManager.hexes[0, 1] = hex;
        }

        for (int i = 1; i < numberOfRows; i++)
        {
            Hex newHex = Instantiate(hexPrefab, transform.position + new Vector3(0f, 0f, 3f * i), Quaternion.identity).GetComponent<Hex>();
            gridManager.hexes[0, hex.relativeCoords.y + 2 * i] = newHex;
            newHex.relativeCoords.x = 0;
            newHex.relativeCoords.y = hex.relativeCoords.y + 2 * i;
            newHex.grabbers = gridManager.grabbers;
        }
        
        iteration++;
    }

    private void Update()
    {
        if (iteration == 24) return;
        
        for (int i = 0; i < numberOfRows; i++)
        {
            Hex newHex = Instantiate(hexPrefab, transform.position + new Vector3(Mathf.Sqrt(3f) * iteration, 0f, 3f * i), Quaternion.identity).GetComponent<Hex>();
            gridManager.hexes[iteration, hex.relativeCoords.y + 2 * i] = newHex;
            newHex.relativeCoords.x = iteration;
            newHex.relativeCoords.y = hex.relativeCoords.y + 2 * i;
            newHex.grabbers = gridManager.grabbers;
        }

        iteration++;
    }
}

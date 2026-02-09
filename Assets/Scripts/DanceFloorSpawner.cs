using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class DanceFloorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject gridTile;
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    // [SerializeField] private GameObject spawner;
    // [SerializeField] private GameObject collectiblePrefab;
    private GridParameters gridParameters;
    private int gridSize;

    private void Start()
    {
        gridParameters = GetComponent<GridParameters>();
        gridSize = (int)gridParameters.gridSize;

        for (int i = 0; i < gridParameters.gridSize; i++)
        {
            for (int j = 0; j < gridParameters.gridSize; j++)
            {
                Instantiate(gridTile, new Vector3(i, transform.position.y, j), Quaternion.identity);
                Instantiate(gridTile, new Vector3(i + gridSize + 1, transform.position.y, j), Quaternion.identity);
            }
        }
        
        float halfSize = (gridParameters.gridSize / 2f) - 0.5f;
        player1.transform.position = new Vector3(halfSize, transform.position.y, halfSize);
        player2.transform.position = new Vector3(gridSize + halfSize + 1, transform.position.y, halfSize);
        
        // for (int i = 0; i < gridParameters.gridSize; i++)
        // {
        //     GameObject spawner1 = Instantiate(spawner, new Vector3(i, transform.position.y, -1), Quaternion.identity);
        //     bulletSpawner.spawners.Add(spawner1);
        //     GameObject spawner2 = Instantiate(spawner, new Vector3(i, transform.position.y, gridParameters.gridSize), Quaternion.identity);
        //     spawner2.transform.Rotate(new Vector3(0, 180, 0));
        //     bulletSpawner.spawners.Add(spawner2);
        //     GameObject spawner3 = Instantiate(spawner, new Vector3(-1, transform.position.y, i), Quaternion.identity);
        //     spawner3.transform.Rotate(new Vector3(0, 90, 0));
        //     bulletSpawner.spawners.Add(spawner3);
        //     GameObject spawner4 = Instantiate(spawner, new Vector3(gridParameters.gridSize, transform.position.y, i), Quaternion.identity);
        //     spawner4.transform.Rotate(new Vector3(0, 270, 0));
        //     bulletSpawner.spawners.Add(spawner4);
        // }
        
        // int randX = Random.Range(0, gridSize);
        // int randZ = Random.Range(0, gridSize);
        // Instantiate(collectiblePrefab, new Vector3(randX, transform.position.y, randZ), Quaternion.identity);
    }
}

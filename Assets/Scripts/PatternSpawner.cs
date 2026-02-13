using System;
using UnityEngine;

public class PatternSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pattern;
    [SerializeField] private int playerNumber;
    private GameObject gm;
    private GridParameters gridParameters;

    public void Start()
    {
        gm = GameObject.Find("GM");
        gridParameters = gm.GetComponent<GridParameters>();
    }

    public void SpawnPattern()
    {
        float halfSize = (gridParameters.gridSize / 2f) - 0.5f;
        
        GameObject patternOrigin = Instantiate(pattern, Vector3.zero, Quaternion.identity);
        
        if (playerNumber == 1)
        {
            patternOrigin.transform.position = new Vector3(gridParameters.gridSize + halfSize + 1, transform.position.y, halfSize);
            patternOrigin.GetComponent<PatternInitializer>().playerNumber = 1;
        }
        else if (playerNumber == 2)
        {
            patternOrigin.transform.position = new Vector3(halfSize, transform.position.y, halfSize);
            patternOrigin.GetComponent<PatternInitializer>().playerNumber = 2;
        }
    }
}

using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectiblePickup : MonoBehaviour
{
    [SerializeField] private GameObject collectiblePrefab;
    private GameObject player;
    private GameObject gm;
    private GridParameters gridParameters;
    private CollectibleCounter counter;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gm = GameObject.Find("GM");
        gridParameters = gm.GetComponent<GridParameters>();
        counter = gm.GetComponent<CollectibleCounter>();
    }

    private void Update()
    {
        if (transform.position == player.transform.position)
        {
            counter.counter++;
            int randX = Random.Range(0, (int)gridParameters.gridSize);
            int randZ = Random.Range(0, (int)gridParameters.gridSize);
            Instantiate(collectiblePrefab, new Vector3(randX, transform.position.y, randZ), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

using System;
using UnityEngine;

public class CameraStartPosition : MonoBehaviour
{
    [SerializeField] private GridParameters gridParameters;
    private float gridSize;

    private void Start()
    {
        gridSize = gridParameters.gridSize + 1;
        float halfSize = (gridParameters.gridSize + 1 / 2f) - 0.5f;
        transform.position = new Vector3(gridSize, transform.position.y, halfSize);
    }
}

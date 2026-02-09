using System;
using UnityEngine;

public class CameraStartPosition : MonoBehaviour
{
    [SerializeField] private GridParameters gridParameters;
    private float gridSize;

    private void Start()
    {
        gridSize = gridParameters.gridSize;
        float halfSize = (gridParameters.gridSize / 2f) - 0.5f;
        transform.position = new Vector3(halfSize, transform.position.y, halfSize);
    }
}

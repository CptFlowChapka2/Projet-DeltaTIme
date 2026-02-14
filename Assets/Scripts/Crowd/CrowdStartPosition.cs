using UnityEngine;

public class CrowdStartPosition : MonoBehaviour
{
    [SerializeField] private GridParameters gridParameters;
    private float gridSize;

    private void Start()
    {
        gridSize = gridParameters.gridSize;
        float halfSize = (gridParameters.gridSize / 2f) - 0.5f;
        transform.position = new Vector3(gridSize, transform.position.y, halfSize);
    }
}

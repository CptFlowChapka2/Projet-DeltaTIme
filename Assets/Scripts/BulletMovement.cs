using System;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private GameObject gm;
    private float gridSize;

    private void Start()
    {
        gm = GameObject.Find("GM");
        gridSize = gm.GetComponent<GridParameters>().gridSize;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * speed));

        if (transform.position.x < -1 ||
            transform.position.x > gridSize ||
            transform.position.z < -1 ||
            transform.position.z > gridSize)
        {
            Destroy(gameObject);
        }
    }
}

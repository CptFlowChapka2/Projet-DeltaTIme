using System;
using UnityEngine;

public class CameraPositionning : MonoBehaviour
{
    [SerializeField] private float smoothTime = 0.1f;
    [SerializeField] private Transform p1;
    [SerializeField] private Transform p2;
    private Vector3 velocity = new Vector3(0, 0, 2);

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, 
            new Vector3 ((p1.position.x + p2.position.x) / 2,
                transform.position.y,
                (-6f) + (p1.position.z + p2.position.z) / 2),
                ref velocity, smoothTime);
    }
}

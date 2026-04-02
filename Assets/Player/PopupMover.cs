using System;
using UnityEngine;

public class PopupMover : MonoBehaviour
{
    public Player playerToFollow;

    private void Update()
    {
        transform.position = new Vector3(playerToFollow.transform.position.x, transform.position.y, playerToFollow.transform.position.z);
    }
}

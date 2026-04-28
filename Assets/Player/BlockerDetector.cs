using System;
using UnityEngine;

public class BlockerDetector : MonoBehaviour
{
    [SerializeField] private bool isRightCollider;
    [SerializeField] private GameObject selfArm;
    [SerializeField] public Player player;
    private SoundManager soundManager;

    private void Start()
    {
        soundManager = FindAnyObjectByType<SoundManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag("ArmBlocker") && obj != selfArm)
        {
            soundManager.collision.Play();
            Knock();
        }
    }
    public void Knock()
    {
        player.state = PlayerState.Knocked;
        if (isRightCollider)
        {
            player.knockedLeft = true;
        }
        else
        {
            player.knockedRight = true;
        }
    }
}

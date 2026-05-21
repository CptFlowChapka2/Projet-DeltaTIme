using System;
using UnityEngine;

public class BlockerDetector : MonoBehaviour
{
    [SerializeField] private bool isRightCollider;
    [SerializeField] private GameObject selfArm;
    [SerializeField] public Player player;
    private SoundManager soundManager;
    [SerializeField] private float timer=0.8f;
    [SerializeField] private float maxTimer=0.8f;

    private void Start()
    {
        soundManager = FindAnyObjectByType<SoundManager>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag("ArmBlocker") && obj != selfArm)
        {
            Knock();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(timer>0) return;
        GameObject obj = other.gameObject;
        if (obj.CompareTag("ArmBlocker") && obj != selfArm)
        {
            timer = maxTimer;
            soundManager.collision.Play();
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

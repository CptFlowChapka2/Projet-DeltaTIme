using System;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float activationTime;
    [SerializeField] private float touchDetection;
    private GameObject player;
    private GameObject gm;
    private CollectibleCounter collectibleCounter;
    private HitsCounter hitsCounter;
    private float gridSize;
    private bool isActivated;
    private float t;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gm = GameObject.Find("GM");
        gridSize = gm.GetComponent<GridParameters>().gridSize;
        collectibleCounter = gm.GetComponent<CollectibleCounter>();
        hitsCounter = gm.GetComponent<HitsCounter>();
    }

    private void Update()
    {
        if (!isActivated)
        {
            t += Time.deltaTime;
            if (t >= activationTime)
            {
                t = 0;
                isActivated = true;
            }
        }
        else
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

        if ((transform.position - player.transform.position).magnitude <= touchDetection)
        {
            hitsCounter.hitsCounter++;
            collectibleCounter.counter -= hitsCounter.numberOfCollectiblesLostPerHit;
            Destroy(gameObject);
        }
    }
}

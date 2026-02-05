using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private MusicParameters musicParameters;
    [SerializeField] private GameObject bulletPrefab;
    public List<GameObject> spawners = new List<GameObject>();
    private float t;

    private void Update()
    {
        t += Time.deltaTime;
        if (t >= 60 / musicParameters.beatsPerMinute)
        {
            t = 0;
            int randomID = Random.Range(0, spawners.Count);
            GameObject bullet = Instantiate(bulletPrefab, spawners[randomID].transform.position, Quaternion.identity);
            bullet.transform.rotation = spawners[randomID].transform.rotation;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward);
    }
}

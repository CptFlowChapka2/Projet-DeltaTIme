using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    private float randomTime;
    private float t;

    private void Start()
    {
        randomTime = Random.Range(1, 10);
    }

    private void Update()
    {
        t += Time.deltaTime;
        if (t >= randomTime)
        {
            t = 0;
            randomTime = Random.Range(1, 10);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.transform.rotation = transform.rotation;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward);
    }
}

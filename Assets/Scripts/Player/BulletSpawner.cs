using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBaseSpawner : MonoBehaviour
{
    //bullet spawn frequency
    [SerializeField]
    private float SpawnDelaySeconds;
    Timer spawnTimer;

    //spawn information support
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;

    void Start()
    {
        bulletSpawnPoint = gameObject.GetComponent<Transform>();

        // create and start timer
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = SpawnDelaySeconds;
        spawnTimer.Run();
    }
    void Update()
    {
        
        if (spawnTimer.Finished) 
        { 
            SpawnBullet();
            spawnTimer.Run();
        }
    }

    void SpawnBullet()
    {
        if (Input.GetAxis("Fire1") != 0)
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.up * bulletSpeed;
        }
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject enemyBulletprefab;
    public float fireRate = 2.0f; // Bullets per second
    private float timeBetweenSpawns;
    private Transform FirePoint;
    public GameObject ExplosionParticle;
    private void Start()
    {
        EnemyObjectPool.Instance.CreatePool(enemyBulletprefab, 50);
        FirePoint = transform.Find("FirePoint");
        timeBetweenSpawns = 1.0f / fireRate; // Calculate time between spawns based on fire rate
    }

    private void Update()
    {
        timeBetweenSpawns -= Time.deltaTime;

        if (timeBetweenSpawns <= 0.0f)
        {
            timeBetweenSpawns = 1.0f / fireRate; // Reset the timer

            GameObject spawnedObject = EnemyObjectPool.Instance.GetPooledObject(enemyBulletprefab);
            if (spawnedObject != null)
            {
                spawnedObject.transform.position = FirePoint.transform.position;
                spawnedObject.SetActive(true);
                Rigidbody objectRigidbody = spawnedObject.GetComponent<Rigidbody>();
                if (objectRigidbody != null)
                {
                    objectRigidbody.velocity = transform.forward * 100;
                }
                else
                {
                    Debug.LogWarning("Spawned object doesn't have a Rigidbody component.");
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "Bullet")
        {
            gameObject.SetActive(false);
            PlayerHealth.Score += 10;
            GameObject Go = Instantiate(ExplosionParticle, transform.position, transform.rotation);
            Destroy(Go, 2f);
        }
    }

    private void OnDisable()
    {
      
    }
}

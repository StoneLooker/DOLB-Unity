using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    private ObjectPool objectPool;
    public float spawnInterval = 2.0f;
    public Transform[] spawnPoints;

    private float timer;

    void Start()
    {
        objectPool = this.GetComponent<ObjectPool>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0;
        }
    }

    void SpawnObstacle()
    {
        GameObject obstacle = objectPool.GetPooledObject();
        if (obstacle != null)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            obstacle.transform.position = spawnPoint.position;
            // obstacle.transform.rotation = spawnPoint.rotation;
            obstacle.SetActive(true);
        }
    }
}

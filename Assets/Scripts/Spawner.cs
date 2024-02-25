using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] collectablePrefabs;
    public GameObject[] hazardPrefabs;
    public float minSpawnInterval = 3f;
    public float maxSpawnInterval = 12f;
    private float spawnInterval;
    private float timer = 0f;
    private float screenWidth = 10f;
    private float spawnHeight = 10f;

    void Start()
    {
        spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnRandomObject();
            timer = 0;
            spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    void SpawnRandomObject()
    {
        if (Random.value < 0.5f)
        {
            SpawnCollectable();
        }
        else
        {
            SpawnHazard();
        }
    }

    void SpawnCollectable()
    {
        if (collectablePrefabs.Length == 0) return;

        int index = Random.Range(0, collectablePrefabs.Length);
        Vector2 spawnPosition = GetSpawnPosition();
        Instantiate(collectablePrefabs[index], spawnPosition, Quaternion.identity);
    }

    void SpawnHazard()
    {
        if (hazardPrefabs.Length == 0) return;

        int index = Random.Range(0, hazardPrefabs.Length);
        Vector2 spawnPosition = GetSpawnPosition();
        Instantiate(hazardPrefabs[index], spawnPosition, Quaternion.identity);
    }

    Vector2 GetSpawnPosition()
    {
        float xPosition = Random.Range(-screenWidth / 2, screenWidth / 2);
        return new Vector2(xPosition, spawnHeight);
    }
}
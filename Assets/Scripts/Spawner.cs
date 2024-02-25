using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] collectablePrefabs;
    public GameObject[] hazardPrefabs;
    public GameObject[] themeBoxPrefabs;
    public float minSpawnInterval = 0.25f;
    public float maxSpawnInterval = 1.25f;
    private float spawnInterval;
    private float timer = 0f;
    private float screenWidth;
    private float spawnHeight;

    void Start()
    {
        ScreenBounds();
        StartCoroutine(StartSpawn(3f));
    }
    void Update()
    {
        if (spawnInterval > 0)
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                SpawnRandomObject();
                timer = 0;
                spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            }
        }
    }

    IEnumerator StartSpawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
    }


    void SpawnRandomObject()
    {
        float rand = Random.value;

        if (rand < 0.33f) SpawnCollectable();
        else if (rand < 0.66f) SpawnHazard();
        else SpawnThemeBox();
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

    void SpawnThemeBox()
    {
        if (themeBoxPrefabs.Length == 0) return;

        int index = Random.Range(0, themeBoxPrefabs.Length);
        Vector2 spawnPosition = GetSpawnPosition();
        Instantiate(themeBoxPrefabs[index], spawnPosition, Quaternion.identity);
    }

    Vector2 GetSpawnPosition()
    {
        float xPosition = Random.Range(-screenWidth / 2, screenWidth / 2);
        return new Vector2(xPosition, spawnHeight);
    }

    void ScreenBounds()
    {
        float height = 2f * Camera.main.orthographicSize;
        screenWidth = height * Camera.main.aspect;

        spawnHeight = Camera.main.orthographicSize + 1f;
    }
}
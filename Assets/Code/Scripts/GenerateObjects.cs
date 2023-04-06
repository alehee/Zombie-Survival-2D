using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObjects : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    public int numObjects = 10;
    public Vector2 spawnBounds = new Vector2(10, 10);

    void Start()
    {
        for (int i = 0; i < numObjects; i++)
        {
            // Losowo wybierz prefab do wygenerowania
            int randomIndex = Random.Range(0, objectPrefabs.Length);
            GameObject objectPrefab = objectPrefabs[randomIndex];

            // Losowo wygeneruj pozycję w obrębie granic spawnu
            float randomX = Random.Range(-spawnBounds.x, spawnBounds.x);
            float randomY = Random.Range(-spawnBounds.y, spawnBounds.y);
            Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

            // Wygeneruj obiekt
            GameObject newObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
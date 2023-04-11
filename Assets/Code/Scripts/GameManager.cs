using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject ZombieStandard;

    [SerializeField]
    int WaveDelay = 10; // seconds
    [SerializeField]
    double WaveZombiesMultiplyer = 1.2;
    int WaveNumber = 0;
    int WaveLastStarted = 0;

    [SerializeField]
    GameObject StickPrefab;
    [SerializeField]
    int PickupableNums = 10;
    Vector2 SpawnBounds = new Vector2(10, 10);

    GameObject[] SpawnPoints;

    int SecondsElapsed = 0;
    float Tick = 0;

    void Start()
    {
        SpawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        GenerateSticks();
        GenerateWave();
    }

    void Update()
    {
        Tick += Time.deltaTime;
        if (Tick >= 1)
        {
            Tick = 0;
            SecondsElapsed += 1;
            Debug.Log($"Timer elapsed: {SecondsElapsed}");

            if(WaveLastStarted + WaveDelay <= SecondsElapsed)
            {
                GenerateWave();
            }
        }
    }

    void GenerateWave()
    {
        WaveNumber++;
        WaveLastStarted = SecondsElapsed;

        RespawnZombies();

        Debug.Log($"Started wave number {WaveNumber}");
    }

    void RespawnZombies()
    {
        int zombiesCount = (int)(WaveNumber * WaveZombiesMultiplyer);
        for (int i = 0; i < zombiesCount; i++)
        {
            int spawnPointNb = Random.Range(0, SpawnPoints.Length);
            Transform zombieSpawnTransform = SpawnPoints[spawnPointNb].transform;
            zombieSpawnTransform.position += new Vector3(i/10, i/10);
            Instantiate(ZombieStandard, zombieSpawnTransform);
        }
    }

    void GenerateSticks()
    {
        for (int i = 0; i < PickupableNums; i++)
        {
            // Losowo wybierz prefab do wygenerowania
            GameObject objectPrefab = StickPrefab;

            // Losowo wygeneruj pozycjê w obrêbie granic spawnu
            float randomX = Random.Range(-SpawnBounds.x, SpawnBounds.x);
            float randomY = Random.Range(-SpawnBounds.y, SpawnBounds.y);
            Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

            // Wygeneruj obiekt
            Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        }
    }
}

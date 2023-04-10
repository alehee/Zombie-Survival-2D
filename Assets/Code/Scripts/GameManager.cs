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

    GameObject[] SpawnPoints;

    int SecondsElapsed = 0;
    float Tick = 0;

    void Start()
    {
        SpawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        RespawnZombies();
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
                RespawnZombies();
            }
        }
    }

    void RespawnZombies()
    {
        WaveNumber++;
        WaveLastStarted = SecondsElapsed;

        int zombiesCount = (int)(WaveNumber * WaveZombiesMultiplyer);
        for (int i = 0; i < zombiesCount; i++)
        {
            int spawnPointNb = Random.Range(0, SpawnPoints.Length);
            Transform zombieSpawnTransform = SpawnPoints[spawnPointNb].transform;
            zombieSpawnTransform.position += new Vector3(i/10, i/10);
            Instantiate(ZombieStandard, zombieSpawnTransform);
        }

        Debug.Log($"Started wave number {WaveNumber}");
    }
}

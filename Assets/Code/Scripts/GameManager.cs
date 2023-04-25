using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject ZombieNormal;

    [SerializeField]
    int WaveDelay = 10; // seconds
    [SerializeField]
    double WaveZombiesMultiplyer = 1.2;
    int WaveNumber = 0;
    int WaveLastStarted = 0;

    [SerializeField]
    GameObject StickPrefab;
    [SerializeField]
    GameObject StonePrefab;
    [SerializeField]
    GameObject ApplePrefab;
    [SerializeField]
    int PickupableNums = 10;
    Vector2 SpawnBounds = new Vector2(10, 10);
    GameObject[] SpawnPoints;
    [SerializeField]
    GameObject Timer;
    [SerializeField]
    TextMeshPro TimerAmount;


    public int SecondsElapsed { get; private set; } = 0;
    float Tick = 0;

    void Start()
    {
        SpawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        GenerateSticks();
        GenerateStones();
        GenerateApples();
        GenerateWave();
    }

    void Update()
    {
        Tick += Time.deltaTime;
        if (Tick >= 1)
        {
            Tick = 0;
            SecondsElapsed += 1;
            TimerAmount.text = SecondsElapsed.ToString();
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
            Instantiate(ZombieNormal, zombieSpawnTransform);
        }
    }

    void GenerateSticks()
    {
        for (int i = 0; i < PickupableNums; i++)
        {
            // Losowo wybierz prefab do wygenerowania
            GameObject objectPrefab = StickPrefab;

            // Losowo wygeneruj pozycj� w obr�bie granic spawnu
            float randomX = Random.Range(-SpawnBounds.x, SpawnBounds.x);
            float randomY = Random.Range(-SpawnBounds.y, SpawnBounds.y);
            Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

            // Wygeneruj obiekt
            Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        }
    }

    void GenerateStones()
    {
        for (int i = 0; i < PickupableNums; i++)
        {
            // Losowo wybierz prefab do wygenerowania
            GameObject objectPrefab = StonePrefab;

            // Losowo wygeneruj pozycj� w obr�bie granic spawnu
            float randomX = Random.Range(-SpawnBounds.x, SpawnBounds.x);
            float randomY = Random.Range(-SpawnBounds.y, SpawnBounds.y);
            Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

            // Wygeneruj obiekt
            Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        }
    }

    void GenerateApples()
    {
        for (int i = 0; i < PickupableNums; i++)
        {
            // Losowo wybierz prefab do wygenerowania
            GameObject objectPrefab = ApplePrefab;

            // Losowo wygeneruj pozycj� w obr�bie granic spawnu
            float randomX = Random.Range(-SpawnBounds.x, SpawnBounds.x);
            float randomY = Random.Range(-SpawnBounds.y, SpawnBounds.y);
            Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

            // Wygeneruj obiekt
            Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        }
    }
}

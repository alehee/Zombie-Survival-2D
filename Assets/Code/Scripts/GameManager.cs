using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    Text TimerAmount;

    [SerializeField]
    GameObject Player;

    [SerializeField]
    GameObject GameOverCamera;

    [SerializeField]
    GameObject WaveAmmountGameObject;
    TextMeshProUGUI WaveText;

    [SerializeField]
    GameObject MenuGameObject;

    [SerializeField]
    GameObject MenuCoinsAmmountGameObject;
    Text MenuCoinsText;

    public int SecondsElapsed { get; private set; } = 0;
    float Tick = 0;

    void Start()
    {
        SpawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        Debug.Log($"Found spawn points {SpawnPoints.Length}");
        WaveText = WaveAmmountGameObject.gameObject.GetComponent<TextMeshProUGUI>();
        MenuCoinsText = MenuCoinsAmmountGameObject.gameObject.GetComponent<Text>();
        MenuPrepare();
    }

    void Update()
    {
        Tick += Time.deltaTime;
        if (Tick >= 1)
        {
            Tick = 0;
            SecondsElapsed += 1;
            TimerAmount.text = "Czas: " + SecondsElapsed.ToString();
            Debug.Log($"Timer elapsed: {SecondsElapsed}");

            if(WaveLastStarted + WaveDelay <= SecondsElapsed)
            {
                GenerateWave();
                GenerateSticks();
                GenerateStones();
                GenerateApples();
            }
        }
    }

    void MenuPrepare()
    {
        var load = SaveManager.Load();
        MenuCoinsText.text = load.Coins.ToString();
    }

    public void MenuStartGame()
    {
        MenuGameObject.SetActive(false);
        Player.SetActive(true);
        StartGame();
    }

    public void MenuQuitGame()
    {
        Application.Quit();
    }

    public void MenuRestart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    void StartGame()
    {
        GenerateSticks();
        GenerateStones();
        GenerateApples();
        GenerateWave();
    }

    void GenerateWave()
    {
        WaveNumber++;
        WaveLastStarted = SecondsElapsed;

        RespawnZombies();

        WaveText.SetText($"Fala {WaveNumber}");
        Debug.Log($"Started wave number {WaveNumber}");
    }

    void RespawnZombies()
    {
        int zombiesCount = (int)(WaveNumber * WaveZombiesMultiplyer);
        Debug.Log($"Zombies to generate {zombiesCount}");
        for (int i = 0; i < zombiesCount; i++)
        {
            int spawnPointNb = Random.Range(0, SpawnPoints.Length);
            Transform zombieSpawnTransform = SpawnPoints[spawnPointNb].transform;
            zombieSpawnTransform.position += new Vector3(i/5, i/5);
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

    public void SetGameOver(Transform position, int coins, int experience, int level)
    {
        GameOverCamera.transform.position = position.position;

        Debug.Log($"Game Over! Resources at the end: {coins} coins, {experience} experience, {level} level, {SecondsElapsed} time");

        GameObject ui = GameOverCamera.transform.Find("Canvas").gameObject;
        ui.transform.Find("Subtext").gameObject.GetComponent<Text>().text = $"LEVEL {level}, COINS {coins}, EXPERIENCE {experience}, TIME {SecondsElapsed}";

        SaveManager.Save(coins);
        GameOverCamera.SetActive(true);
    }
}

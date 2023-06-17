using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject ZombieNormal;
    [SerializeField]
    GameObject ZombieFast;
    [SerializeField]
    GameObject ZombieTank;

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
    int PickupableNums = 7;
    Vector2 SpawnBounds = new Vector2(30, 30);
    GameObject[] SpawnPoints;
    [SerializeField]
    Text TimerAmount;

    [SerializeField]
    GameObject Player;

    [SerializeField]
    GameObject WeaponBow;

    [SerializeField]
    GameObject WeaponSpear;

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

    [SerializeField]
    GameObject MenuSelectedWeaponGameObject;
    Text MenuSelectedWeaponText;

    [SerializeField]
    GameObject MenuUpgradeButtonTextGameObject;
    Text MenuUpgradeButtonText;

    [SerializeField]
    GameObject MenuSelectBowGameObject;
    Image MenuSelectBowImage;

    [SerializeField]
    GameObject MenuSelectSpearGameObject;
    Image MenuSelectSpearImage;

    private int menuLevelBow, menuLevelSpear;
    string menuSelectedWeapon = "";
    int menuCoins = 0;
    int menuNextLevelUpgradeCost = 0;

    public int SecondsElapsed { get; private set; } = 0;
    float Tick = 0;

    bool GameStarted = false;

    void Start()
    {
        SpawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        Debug.Log($"Found spawn points {SpawnPoints.Length}");
        WaveText = WaveAmmountGameObject.gameObject.GetComponent<TextMeshProUGUI>();
        MenuCoinsText = MenuCoinsAmmountGameObject.gameObject.GetComponent<Text>();
        MenuSelectedWeaponText = MenuSelectedWeaponGameObject.gameObject.GetComponent<Text>();
        MenuUpgradeButtonText = MenuUpgradeButtonTextGameObject.gameObject.GetComponent<Text>();
        MenuSelectBowImage = MenuSelectBowGameObject.gameObject.gameObject.GetComponent<Image>();
        MenuSelectSpearImage = MenuSelectSpearGameObject.gameObject.GetComponent<Image>();
        MenuPrepare();
    }

    void Update()
    {
        if (GameStarted)
        {
            Tick += Time.deltaTime;
            if (Tick >= 1)
            {
                Tick = 0;
                SecondsElapsed += 1;
                TimerAmount.text = "Czas: " + SecondsElapsed.ToString();
                Debug.Log($"Timer elapsed: {SecondsElapsed}");

                if (WaveNumber == 0 || WaveLastStarted + WaveDelay <= SecondsElapsed)
                {
                    GenerateWave();
                    GenerateSticks();
                    GenerateStones();
                    GenerateApples();
                }
            }
        }
    }

    void MenuPrepare()
    {
        var load = SaveManager.Load();
        menuCoins = load.Coins;
        MenuCoinsText.text = menuCoins.ToString();
        menuLevelBow = load.BowLevel;
        menuLevelSpear = load.SpearLevel;

        if (menuSelectedWeapon.StartsWith("BOW"))
        {
            menuSelectedWeapon = "BOW, LEVEL " + menuLevelBow;
            menuNextLevelUpgradeCost = menuLevelBow * 15;
        }
            
        else if (menuSelectedWeapon.StartsWith("SPEAR"))
        {
            menuSelectedWeapon = "SPEAR, LEVEL " + menuLevelSpear;
            menuNextLevelUpgradeCost = menuLevelSpear * 15;
        }

        MenuSelectedWeaponText.text = menuSelectedWeapon;
        MenuUpgradeButtonText.text = $"Upgrade for {menuNextLevelUpgradeCost} coins";
    }

    public void MenuWeaponSetBow()
    {
        SoundManagerScript.PlaySound ("insufficient_materials");
        menuSelectedWeapon = "BOW";
        MenuSelectBowImage.color = new Color(1, 1, 1);
        MenuSelectSpearImage.color = new Color(0.5f, 0.5f, 0.5f);
        MenuPrepare();
    }

    public void MenuWeaponSetSpear()
    {
        SoundManagerScript.PlaySound ("insufficient_materials");
        menuSelectedWeapon = "SPEAR";
        MenuSelectBowImage.color = new Color(0.5f, 0.5f, 0.5f);
        MenuSelectSpearImage.color = new Color(1, 1, 1);
        MenuPrepare();
    }

    public void MenuWeaponUpgrade()
    {
        if ((menuSelectedWeapon.StartsWith("BOW") || menuSelectedWeapon.StartsWith("SPEAR")) && menuCoins >= menuNextLevelUpgradeCost)
        {
            if (menuSelectedWeapon.StartsWith("BOW"))
                menuLevelBow += 1;
            else if (menuSelectedWeapon.StartsWith("SPEAR"))
                menuLevelSpear += 1;
            SaveManager.Save(-menuNextLevelUpgradeCost, menuLevelBow, menuLevelSpear);
            MenuPrepare();
            SoundManagerScript.PlaySound ("insufficient_materials");
        }
    }

    public void MenuStartGame()
    {
        if (menuSelectedWeapon.StartsWith("BOW") || menuSelectedWeapon.StartsWith("SPEAR"))
        {
            MenuGameObject.SetActive(false);
            Player.SetActive(true);
            if (menuSelectedWeapon.StartsWith("BOW"))
            {
                WeaponBow.gameObject.GetComponent<WeaponBow>().Damage += menuLevelBow * 0.5f;
                WeaponBow.gameObject.GetComponent<WeaponBow>().SetLevel(menuLevelBow);
                GameObject w = Instantiate(WeaponBow, Player.transform);
            }
            else if (menuSelectedWeapon.StartsWith("SPEAR"))
            {
                WeaponSpear.gameObject.GetComponent<WeaponSpear>().Damage += menuLevelSpear * 0.5f;
                WeaponSpear.gameObject.GetComponent<WeaponSpear>().SetLevel(menuLevelSpear);
                GameObject w = Instantiate(WeaponSpear, Player.transform);
            }
            SoundManagerScript.PlaySound ("insufficient_materials");
            GameStarted = true;
        }
    }

    public void MenuQuitGame()
    {
        Application.Quit();
    }

    public void MenuRestart()
    {
        Application.LoadLevel(Application.loadedLevel);
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

            float randomNumber = Random.Range(0f, 1f);
            if(randomNumber <= 0.6 )
                Instantiate(ZombieNormal, zombieSpawnTransform);
            else if(randomNumber <= 0.87)
                Instantiate(ZombieFast, zombieSpawnTransform);
            else
                Instantiate(ZombieTank, zombieSpawnTransform);
        }
    }

    void GenerateSticks()
    {
        Vector3 playerPosition = Player.transform.position;
        for (int i = 0; i < PickupableNums; i++)
        {
            // Losowo wybierz prefab do wygenerowania
            GameObject objectPrefab = StickPrefab;

            // Losowo wygeneruj pozycj� w obr�bie granic spawnu
            float randomX = Random.Range(-SpawnBounds.x, SpawnBounds.x);
            float randomY = Random.Range(-SpawnBounds.y, SpawnBounds.y);
            Vector3 spawnPosition = new Vector3(playerPosition.x + randomX, playerPosition.y + randomY, 0);

            // Wygeneruj obiekt
            Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        }
    }

    void GenerateStones()
    {
        Vector3 playerPosition = Player.transform.position;
        for (int i = 0; i < PickupableNums; i++)
        {
            // Losowo wybierz prefab do wygenerowania
            GameObject objectPrefab = StonePrefab;

            // Losowo wygeneruj pozycj� w obr�bie granic spawnu
            float randomX = Random.Range(-SpawnBounds.x, SpawnBounds.x);
            float randomY = Random.Range(-SpawnBounds.y, SpawnBounds.y);
            Vector3 spawnPosition = new Vector3(playerPosition.x + randomX, playerPosition.y + randomY, 0);

            // Wygeneruj obiekt
            Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        }
    }

    void GenerateApples()
    {
        Vector3 playerPosition = Player.transform.position;
        for (int i = 0; i < PickupableNums; i++)
        {
            // Losowo wybierz prefab do wygenerowania
            GameObject objectPrefab = ApplePrefab;

            // Losowo wygeneruj pozycj� w obr�bie granic spawnu
            float randomX = Random.Range(-SpawnBounds.x, SpawnBounds.x);
            float randomY = Random.Range(-SpawnBounds.y, SpawnBounds.y);
            Vector3 spawnPosition = new Vector3(playerPosition.x + randomX, playerPosition.y + randomY, 0);

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

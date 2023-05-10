using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerStatus : Status
{
    PlayerMovement PlayerMovement;
    
    [SerializeField]
    GameObject SticksAmountGameObject;
    TextMeshProUGUI SticksText;

    [SerializeField]
    GameObject StonesAmountGameObject;
    TextMeshProUGUI StonesText;

    [SerializeField]
    GameObject CoinsAmmountGameObject;
    TextMeshProUGUI CoinsText;

    [SerializeField]
    GameObject ExperienceAmmountGameObject;
    TextMeshProUGUI ExperienceText;

    [SerializeField]
    GameObject LevelGameObject;
    TextMeshProUGUI LevelText;

    Dictionary<string, int> Resources;

    int Level = 1;
    [SerializeField]
    int NextLevelExperienceMultiplyer = 5;
    int TotalExperience = 0;

    private int Sticks { get { return Resources["Sticks"]; } }
    private int Stones { get { return Resources["Stones"]; } }

    GameManager GameManager;

    void Start()
    {
        Resources = new Dictionary<string, int> { { "Sticks", 0 }, { "Stones", 0 }, { "Coins", 0 }, { "Exp", 0 } };
        PlayerMovement = gameObject.GetComponent<PlayerMovement>();
        MaxHealth = GetHealth();
        GameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        SticksText = SticksAmountGameObject.gameObject.GetComponent<TextMeshProUGUI>();
        StonesText = StonesAmountGameObject.gameObject.GetComponent<TextMeshProUGUI>();
        CoinsText = CoinsAmmountGameObject.gameObject.GetComponent<TextMeshProUGUI>();
        ExperienceText = ExperienceAmmountGameObject.gameObject.GetComponent<TextMeshProUGUI>();
        LevelText = LevelGameObject.gameObject.GetComponent<TextMeshProUGUI>();
        HealthText = HealthGameObject.gameObject.GetComponent<TextMeshProUGUI>();

        UpdateHealthCounter();
    }

    private void OnDestroy()
    {
        GameManager.SetGameOver(this.transform, Resources["Coins"], TotalExperience, Level);
    }

    #region Gathering
    public void AddStick()
    {
        Resources["Sticks"]++;
        Debug.Log($"Gathered a stick! In eq: {Resources["Sticks"]}");
        UpdateSticksCounter();
    }

    public void AddStone()
    {
        Resources["Stones"]++;
        Debug.Log($"Gathered a stone! In eq: {Resources["Stones"]}");
        UpdateStonesCounter();
    }

    public void AddCoin()
    {
        Resources["Coins"]++;
        Debug.Log($"Gathered a coin! In eq: {Resources["Coins"]}");
        UpdateCoinsCounter();
    }

    public void AddExp()
    {
        Resources["Exp"]++;
        TotalExperience++;
        Debug.Log($"Gathered an exp! In eq: {Resources["Exp"]}");
        UpdateExperienceCounter();
        if (Resources["Exp"] >= (5 + NextLevelExperienceMultiplyer*Level))
        {
            NextLevel();
            Debug.Log($"Level up! Current level: {Level}");
        }
    }

    public void SetSticks(int sticks)
    {
        Resources["Sticks"] = sticks;
    }

    public int GetSticks()
    {
        return Sticks;
    }

    public void SetStones(int stones)
    {
        Resources["Stones"] = stones;
    }

    public int GetStones()
    {
        return Stones;
    }

    public void UpdateSticksCounter()
    {
        int SticksCount = Resources["Sticks"];
        SticksText.SetText(SticksCount.ToString());
        Debug.Log($"StickCounter Updated! In eq: {SticksCount}");
    }

    public void UpdateStonesCounter()
    {
        int StonesCount = Resources["Stones"];
        StonesText.SetText(StonesCount.ToString());
        Debug.Log($"StickCounter Updated! In eq: {StonesCount}");
    }

    public void UpdateCoinsCounter()
    {
        int count = Resources["Coins"];
        CoinsText.SetText(count.ToString());
    }

    public void UpdateExperienceCounter()
    {
        int count = Resources["Exp"];
        ExperienceText.SetText(count.ToString());
    }

    public void UpdateLevelCounter()
    {
        LevelText.SetText(Level.ToString());
    }
    #endregion

    #region Leveling
    private void NextLevel()
    {
        Resources["Exp"] = 0;
        Level++;
        PlayerMovement.speed += 0.1f;
        MaxHealth += 2;
        GainHealth(2);
        UpdateLevelCounter();
    }
    #endregion
}

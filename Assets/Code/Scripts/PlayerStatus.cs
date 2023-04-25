using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerStatus : Status
{
    PlayerMovement PlayerMovement;

    [SerializeField]
    GameObject SticksCounter;
    [SerializeField]
    TextMeshPro SticksAmount;

    Dictionary<string, int> Resources;

    int Level = 1;
    [SerializeField]
    int NextLevelExperienceMultiplyer = 5;

    private int Sticks { get { return Resources["Sticks"]; } }

    void Start()
    {
        Resources = new Dictionary<string, int> { { "Sticks", 0 }, { "Stones", 0 }, { "Coins", 0 }, { "Exp", 0 } };
        PlayerMovement = gameObject.GetComponent<PlayerMovement>();
        MaxHealth = GetHealth();
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
        UpdateSticksCounter();
    }

    public void AddCoin()
    {
        Resources["Coins"]++;
        Debug.Log($"Gathered a coin! In eq: {Resources["Coins"]}");
    }

    public void AddExp()
    {
        Resources["Exp"]++;
        Debug.Log($"Gathered an exp! In eq: {Resources["Exp"]}");
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

    public void UpdateSticksCounter()
    {
        int SticksCount = Resources["Sticks"];
        SticksAmount.text = SticksCount.ToString();
        Debug.Log($"StickCounter Updated! In eq: {Resources["Sticks"]}");
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
    }
    #endregion
}

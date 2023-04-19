using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Status
{
    Dictionary<string, int> Resources;

    public int Sticks { get { return Resources["Sticks"]; } }

    void Start()
    {
        Resources = new Dictionary<string, int> { { "Sticks", 0 }, { "Stones", 0 }, { "Coins", 0 }, { "Exp", 0 } };
    }

    public void AddStick()
    {
        Resources["Sticks"]++;
        Debug.Log($"Gathered a stick! In eq: {Resources["Sticks"]}");
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
    }

    public void SetSticks(int sticks)
    {
        Resources["Sticks"] = sticks;
    }
}

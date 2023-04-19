using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerStatus : Status
{

    [SerializeField]
    GameObject SticksCounter;
    [SerializeField]
    TextMeshPro SticksAmount;

    Dictionary<string, int> Resources;

    private int Sticks { get { return Resources["Sticks"]; } }

    void Start()
    {
        Resources = new Dictionary<string, int> { { "Sticks", 0 }, { "Stones", 0 } };
    }

    public void AddStick()
    {
        Resources["Sticks"]++;
        Debug.Log($"Gathered a stick! In eq: {Resources["Sticks"]}");
        int SticksCount = Resources["Sticks"];
        SticksAmount.text = SticksCount.ToString();
    }

    public void SetSticks(int sticks)
    {
        Resources["Sticks"] = sticks;
    }

    public int GetSticks()
    {
        return Sticks;
    }
    
}

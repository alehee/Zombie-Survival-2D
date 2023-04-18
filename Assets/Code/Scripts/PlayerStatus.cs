using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Status
{
    Dictionary<string, int> Resources;

    public int Sticks { get { return Resources["Sticks"]; } }

    void Start()
    {
        Resources = new Dictionary<string, int> { { "Sticks", 0 }, { "Stones", 0 } };
    }

    public void AddStick()
    {
        Resources["Sticks"]++;
        Debug.Log($"Gathered a stick! In eq: {Resources["Sticks"]}");
    }

    public void SetSticks(int sticks)
    {
        Resources["Sticks"] = sticks;
    }
}

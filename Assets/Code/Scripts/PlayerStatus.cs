using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Status
{
    Dictionary<string, int> Resources;

<<<<<<< HEAD
    public int Sticks { get { return Resources["Sticks"]; } }

=======
>>>>>>> 01470450649ada7e03cbdabf16fd673f55ebb4b7
    void Start()
    {
        Resources = new Dictionary<string, int> { { "Sticks", 0 }, { "Stones", 0 } };
    }

    public void AddStick()
    {
        Resources["Sticks"]++;
        Debug.Log($"Gathered a stick! In eq: {Resources["Sticks"]}");
    }
<<<<<<< HEAD

    public void SetSticks(int sticks)
    {
        Resources["Sticks"] = sticks;
    }
=======
>>>>>>> 01470450649ada7e03cbdabf16fd673f55ebb4b7
}

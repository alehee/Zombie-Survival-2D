using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStatus : Status
{
    private void Start()
    {
        MaxHealth = Health;
        ZombieHealthGreenBar = HealthGameObject.transform.Find("Green").gameObject;
    }
}

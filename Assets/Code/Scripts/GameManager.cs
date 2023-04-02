using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject zombieStandard;

    [SerializeField]
    int waveDelay = 30; // seconds
    [SerializeField]
    double waveZombiesMultiplyer = 1.5;

    int secondsElapsed = 0;
    float tick = 0;

    void Start()
    {
        
    }

    void Update()
    {
        tick += Time.deltaTime;
        if (tick >= 1)
        {
            tick = 0;
            secondsElapsed += 1;

            Debug.Log($"Timer elapsed: {secondsElapsed}");
        }
    }
}

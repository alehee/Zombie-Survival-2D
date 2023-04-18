using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerTmp : MonoBehaviour
{
    private GameManager gameManager;
    public TextMeshPro time;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        int secondsElapsed = gameManager.SecondsElapsed;
        time.text = secondsElapsed.ToString();
        // zrobić coś z wartością SecondsElapsed
    }
}

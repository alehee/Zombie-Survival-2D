using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SticksTmp : MonoBehaviour
{
    private PlayerStatus status;
    public TextMeshPro amount;

    void Start()
    {
        status = FindObjectOfType<PlayerStatus>();
    }

    void Update()
    {
        int SticksAmount = status.Sticks;
        amount.text = SticksAmount.ToString();
    }
}

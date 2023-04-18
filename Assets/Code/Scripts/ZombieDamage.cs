using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDamage : MonoBehaviour
{
    [SerializeField]
    double Damage = 1;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerStatus>(out PlayerStatus playerStatus))
        {
            playerStatus.TakeDamage(Damage);
            Debug.Log($"Damage dealt to player: {Damage}");
        }

    }
}

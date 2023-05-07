using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBowArrow : MonoBehaviour
{
    [SerializeField]
    double Damage = 3;

    void Start()
    {
        Destroy(gameObject, 2);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        if (collision.gameObject.TryGetComponent<Status>(out Status status))
        {
            status.TakeDamage(Damage);
            Debug.Log($"Damage dealt to {collision.gameObject.name}: {Damage}");
        }
        Destroy(gameObject);
    }
}
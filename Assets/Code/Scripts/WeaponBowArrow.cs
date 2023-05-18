using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponBowArrow : MonoBehaviour
{
    [SerializeField]
    double Damage = 3;

    string[] tagsToIgnore = new string[] { "Player", "Bullet" };

    void Start()
    {
        Destroy(gameObject, 2);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (tagsToIgnore.Contains(collision.gameObject.tag) || collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Building"))
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowArrow : MonoBehaviour
{
    [SerializeField]
    private double Damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Status>(out Status status))
        {
            status.TakeDamage(Damage);
        }
        Destroy(gameObject);
    }
}
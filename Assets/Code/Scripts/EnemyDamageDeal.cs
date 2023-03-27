using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageDeal : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<EnemyDamage>(out EnemyDamage enemyComponent))
        {
            enemyComponent.TakeDamage(1);
        }

    }

}
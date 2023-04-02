using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField]
    double Health = 10.0;

    public void TakeDamage(double damage)
    {
        Health -= damage;

        if(Health <= 0)
        {
            Destroy(gameObject);
            Debug.Log($"Object {gameObject.name} destroyed due to health below 0");
        }
    }
}

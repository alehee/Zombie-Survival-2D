using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBow : Weapon
{
    [SerializeField]
    GameObject arrow;
    [SerializeField]
    Transform firePoint;
    [SerializeField]
    float fireForce;

    void Start()
    {
        Weapon.Start();
        weaponGameObject = gameObject;
    }

    void Update()
    {
        Weapon.Update();
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        GameObject projectile = Instantiate(arrow, firePoint.position, firePoint.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    } 
}
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
    List<Transform> firePointsUlt;
    [SerializeField]
    float fireForce;

    float drawTime = 0;

    public double Damage = 3;

    void Start()
    {
        Weapon.Start();
        weaponGameObject = gameObject;
    }

    void Update()
    {
        Weapon.Update();

        if (Input.GetMouseButtonDown(1) && UseUltimate())
            Ultimate();
        else if (Input.GetMouseButton(0))
        {
            drawTime += Time.deltaTime;
        }
        else if (drawTime > 0)
        {
            if (drawTime > 1)
                drawTime = 1;

            Shoot(fireForce * drawTime, firePoint);

            drawTime = 0;
        }
    }

    public void SetLevel(int l)
    {
        level = l;
    }

    protected override void Ultimate()
    {
        Shoot(fireForce, firePoint);
        Shoot(fireForce, firePointsUlt[0]);
        Shoot(fireForce, firePointsUlt[1]);
        Debug.Log("Used ultimate!");
    }

    public void Shoot(float force, Transform fp)
    {
        GameObject projectile = Instantiate(arrow, fp.position, fp.rotation);
        projectile.GetComponent<WeaponBowArrow>().Damage = Damage;
        projectile.GetComponent<Rigidbody2D>().AddForce(fp.up * force, ForceMode2D.Impulse);
        Debug.Log($"Arrow fired with force {force}");
    }
}
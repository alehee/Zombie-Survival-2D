using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpear : Weapon
{
    public float Damage = 3;

    GameObject playerGameObject;

    List<string> tagsToIgnore = new List<string> { "Player", "Bullet" };

    PlayerStatus playerStatus;

    void Start()
    {
        Weapon.Start();
        weaponGameObject = gameObject;
        playerGameObject = gameObject.transform.parent.gameObject;
        playerStatus = playerGameObject.GetComponent<PlayerStatus>();
    }

    void Update()
    {
        Weapon.Update();

        if (Input.GetMouseButtonDown(1) && UseUltimate())
            Ultimate();
    }

    public void SetLevel(int l)
    {
        level = l;
    }

    protected override void Ultimate()
    {
        var position = GetMousePoint();
        playerGameObject.transform.position = new Vector3(position.x, position.y, -1f);
        Debug.Log("Used ultimate!");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (tagsToIgnore.Contains(collision.gameObject.tag))
        {
            return;
        }

        if (collision.gameObject.TryGetComponent<Status>(out Status status))
        {
            status.TakeDamage(Damage);
            playerStatus.GainHealth(1);
            Debug.Log($"Damage dealt to {collision.gameObject.name}: {Damage}");
        }
    }
}
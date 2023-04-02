using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected static Camera playerCamera;
    protected static Rigidbody2D playerRigidbody;
    protected static Vector2 mousePosition;
    protected static GameObject weaponGameObject;
    protected static int rotationSpeed = 15;

    protected static void Start()
    {
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerCamera = GameObject.Find("Player Camera").GetComponent<Camera>();
    }

    protected static void Update()
    {
        mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = mousePosition - playerRigidbody.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);
        weaponGameObject.transform.rotation = Quaternion.Slerp(weaponGameObject.transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
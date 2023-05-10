using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected static Camera playerCamera;
    protected static Rigidbody2D playerRigidbody;
    protected static Vector2 mousePosition;
    protected static GameObject weaponGameObject;
    protected static int rotationSpeed = 15;
    protected static int level { get; set; } = 1;
    [SerializeField]
    private static float ultimateRestartTime = 5;
    protected static float ultimateCooldown { get; set; } = ultimateRestartTime;
    static TextMeshProUGUI ultimateCooldownText { get; set; }

    protected static void Start()
    {
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerCamera = GameObject.Find("Player Camera").GetComponent<Camera>();
        ultimateCooldownText = GameObject.Find("HUD/CD Broni/Text (TMP)").gameObject.GetComponent<TextMeshProUGUI>();
    }

    protected static void Update()
    {
        RotateWeapon();
        if (ultimateCooldown > 0)
        {
            ultimateCooldown -= Time.deltaTime;
            if (ultimateCooldown < 0)
                ultimateCooldown = 0;
        }
        ultimateCooldownText.SetText(ultimateCooldown.ToString("0.00"));
    }

    private static void RotateWeapon()
    {
        mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = mousePosition - playerRigidbody.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);
        weaponGameObject.transform.rotation = Quaternion.Slerp(weaponGameObject.transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    protected static bool UseUltimate() 
    {
        if (level >= 10 && ultimateCooldown <= 0)
        {
            ultimateCooldown = ultimateRestartTime;
            return true;
        }

        Debug.Log($"Cannot use ultimate, level {level}, cooldown {ultimateCooldown}");
        return false;
    }

    protected virtual void Ultimate()
    {
        Debug.Log("Ultimate for this weapon is not prepared!");
    }

    protected static void _LevelMax()
    {
        level = 10;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public float speed;
    public Weapon weapon;
    public Camera sceneCamera;
    private Rigidbody2D rigidbody;
    private Vector2 mousePosition;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
        }

        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 aimDirection = mousePosition - rigidbody.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rigidbody.rotation = aimAngle;
    }
}

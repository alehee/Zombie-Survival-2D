using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed;
    Rigidbody2D rigidbody;
    PlayerStatus status;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        status = GetComponent<PlayerStatus>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rigidbody.velocity = new Vector2(moveHorizontal*speed, moveVertical*speed);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Pickupable"))
        {
            switch (collider.gameObject.name)
            {
                case "Stick":
                    status.AddStick();
                    break;
            }

            Destroy(collider.gameObject);
        }
    }
}

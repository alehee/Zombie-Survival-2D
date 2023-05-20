using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigidbody;
    PlayerStatus status;

    [SerializeField]
    GameObject PlayerModel;
    Animator PlayerAnimator;
    SpriteRenderer SpriteRenderer;

    float lastMoveVertical = 0;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        status = GetComponent<PlayerStatus>();
        PlayerAnimator = PlayerModel.GetComponent<Animator>();
        SpriteRenderer = PlayerModel.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (moveHorizontal != 0 || moveVertical != 0)
        {
            PlayerAnimator.speed = 1;
        }
        else
        {
            PlayerAnimator.speed = 0;
        }

        if (moveHorizontal > lastMoveVertical)
            SpriteRenderer.flipX = false;
        else
            SpriteRenderer.flipX = true;

        rigidbody.velocity = new Vector2(moveHorizontal*speed, moveVertical*speed);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Pickupable"))
        {
            switch (collider.gameObject.name)
            {
                case "Stick(Clone)":
                    status.AddStick();
                    break;
                case "Stone(Clone)":
                    status.AddStone();
                    break;
                case "Apple(Clone)":
                    status.GainHealth(1);
                    break;
            }
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.CompareTag("Exp"))
        {
            status.AddExp();
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.CompareTag("Coin"))
        {
            status.AddCoin();
            Destroy(collider.gameObject);
        }
    }
}

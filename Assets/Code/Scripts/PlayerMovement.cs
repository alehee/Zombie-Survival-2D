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
            if (!SoundManagerScript.audioSrc.isPlaying)
            {
                SoundManagerScript.PlaySound ("step");
            }
            else
            {
                SoundManagerScript.StopMusic();
            }
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
                    SoundManagerScript.PlaySound ("wood");
                    status.AddStick();
                    break;
                case "Stone(Clone)":
                    SoundManagerScript.PlaySound ("stone");
                    status.AddStone();
                    break;
                case "Apple(Clone)":
                    SoundManagerScript.PlaySound ("apple");
                    status.GainHealth(1);
                    break;
            }
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.CompareTag("Exp"))
        {
            status.AddExp();
            SoundManagerScript.PlaySound ("exp_coin");
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.CompareTag("Coin"))
        {
            status.AddCoin();
            SoundManagerScript.PlaySound ("coin");
            Destroy(collider.gameObject);
        }
    }
}

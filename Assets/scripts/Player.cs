using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D playerRigidbody2D;
    public BoxCollider2D BoxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var HorizontalInput = Input.GetAxis("Horizontal");

        if (HorizontalInput != 0)
        {
            animator.SetBool("Walking", true);
            spriteRenderer.flipX = (HorizontalInput < 0);
            float vel = 2;
            if (HorizontalInput < 0)
            {
                vel *= -1;
            }
            playerRigidbody2D.velocity = (new Vector2(vel, playerRigidbody2D.velocity.y));
        }
        else
        {
            playerRigidbody2D.velocity = new Vector2(0, playerRigidbody2D.velocity.y);
            animator.SetBool("Walking", false);
        }

        if (Input.GetKey(KeyCode.UpArrow) && !animator.GetBool("Jumping"))
        {
            animator.SetBool("Jumping", true);
            float vel = 5;
            playerRigidbody2D.velocity = (new Vector2(playerRigidbody2D.velocity.x, vel));
        }

        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("Attacking", true);
        }
        else
        {
            animator.SetBool("Attacking", false);
        }
    }
}

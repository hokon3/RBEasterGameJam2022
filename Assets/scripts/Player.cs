using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D playerRigidbody2D;
    public BoxCollider2D BoxCollider2D;
    public BoxCollider2D Ground;
    public BoxCollider2D SwordRange;
    public bool DoDamage;
    public GameObject EndText;

    private int HitPoints;
    private bool Invulnerable;

    // Start is called before the first frame update
    void Start()
    {
        HitPoints = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (HitPoints <= 0)
        {
            playerRigidbody2D.velocity = new Vector2(0, playerRigidbody2D.velocity.y);
            return;
        }
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
        else if (BoxCollider2D.IsTouching(Ground)){
            animator.SetBool("Jumping", false);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("Attacking", true);
        }
        else
        {
            animator.SetBool("Attacking", false);
        }

        if (DoDamage)
        {
            var soldiers = GameObject.FindGameObjectsWithTag("Soldier");
            foreach (var item in soldiers)
            {
                item.BroadcastMessage("KillSoldier");
            }
        }
    }

    void TakeDamage()
    {
        if (Invulnerable)
        {
            return;
        }
        HitPoints--;
        Invulnerable = true;
        animator.SetBool("Damaged", true);
        if (HitPoints <= 0)
        {
            animator.SetBool("Dead", true);
            EndText.SetActive(true);
            gameObject.GetComponent<AudioSource>().Play();
        }
        else
        {
            StartCoroutine("Waiter");
        }
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(1);
        Invulnerable = false;
        animator.SetBool("Damaged", false);
    }

    void StartGame()
    {
        var soldiers = GameObject.FindGameObjectsWithTag("Soldier");
        foreach (var item in soldiers)
        {
            item.BroadcastMessage("StartGame");
        }
    }
}

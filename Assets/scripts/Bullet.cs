using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool isGoingLeft;
    private int timer;
    private CircleCollider2D CircleCollider2D;
    private BoxCollider2D Player;

    // Start is called before the first frame update
    void Start()
    {
        timer = 2000;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
        CircleCollider2D = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer--;
        if (timer < 0)
        {
            Destroy(gameObject);
        }
        if (CircleCollider2D.IsTouching(Player))
        {
            Player.BroadcastMessage("TakeDamage");
        }

        float distance = 1 * Time.deltaTime;
        if (isGoingLeft)
        {
            distance *= -1;
        }
        Vector3 temp = transform.position;

        temp.x += distance;

        transform.position = temp;
    }

    void GoingLeft()
    {
        isGoingLeft = true;
    }
}

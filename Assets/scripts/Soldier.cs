using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public GameObject PrefabBullet;

    private bool seesPlayer;
    private Transform playerTransform;
    private int delay;
    private BoxCollider2D soldierBox;
    private BoxCollider2D damageBox;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        damageBox = GameObject.FindGameObjectWithTag("Damager").GetComponent<BoxCollider2D>();
        soldierBox = gameObject.GetComponent<BoxCollider2D>();
        seesPlayer = true;
        delay = 0;
    }

    // Update is called once per frame
    void Update()
    {
        delay--;
        if (seesPlayer && delay <= 0)
        {
            SpriteRenderer.flipX = playerTransform.position.x < transform.position.x;
   
            var bullet = Instantiate(PrefabBullet, transform, false);
            var bulletPosition = bullet.transform.localPosition;
            if (SpriteRenderer.flipX)
            {
                bulletPosition.x *= -1f;
                bullet.BroadcastMessage("GoingLeft");
            }
            bullet.transform.localPosition = bulletPosition;
            
            delay = 1000;
        }

    }

    void KillSoldier()
    {
        if (damageBox.IsTouching(soldierBox))
        {
            Destroy(gameObject);
        }
    }
}

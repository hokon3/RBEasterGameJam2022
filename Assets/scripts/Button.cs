using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public CircleCollider2D CircleCollider2D;
    public BoxCollider2D BoxCollider2D;
    public SpriteRenderer SpriteRenderer;
    public Sprite Sprite;
    public SpriteRenderer nukeImage;
    public GameObject EndText;
    public AudioSource AudioSource;
    public AudioSource backgroundMusic;

    private bool isTriggered;

    // Start is called before the first frame update
    void Start()
    {
        isTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CircleCollider2D.IsTouching(BoxCollider2D) && !isTriggered)
        {
            isTriggered = true;
            SpriteRenderer.sprite = Sprite;
            nukeImage.enabled = true;
            EndText.SetActive(true);
            AudioSource.Play();
            backgroundMusic.Stop();
        }
    }
}

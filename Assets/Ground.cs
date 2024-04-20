using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Vector2 startSize;

    private float speed = 1.3f;
    private float width = 6f;

    private void Start()
    {
        startSize = new Vector2(spriteRenderer.size.x, spriteRenderer.size.y);
    }

    private void Update()
    {
        spriteRenderer.size = new Vector2(spriteRenderer.size.x + speed * Time.deltaTime, spriteRenderer.size.y);

        if (spriteRenderer.size.x > width)
        {
            spriteRenderer.size = startSize;
        }
    }
}

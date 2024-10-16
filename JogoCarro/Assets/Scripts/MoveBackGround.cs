using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackGround : MonoBehaviour
{
    [SerializeField] private float speedBG = 1f;
    [SerializeField] private float width = 6f;

    private SpriteRenderer spriteRenderer;

    private Vector2 startSize;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        startSize = new Vector2(spriteRenderer.size.x, spriteRenderer.size.y);
    }

    private void Update()
    {
        startSize = new Vector2(spriteRenderer.size.x + speedBG * Time.deltaTime, spriteRenderer.size.y);

        if (spriteRenderer.size.x > width) 
        {
            spriteRenderer.size = startSize;
        }
    }
}

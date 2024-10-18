using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackGround : MonoBehaviour
{
    [SerializeField] private float speedBackGround;
    private SpriteRenderer sr;
    private Vector2 playY;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        playY = new Vector2(sr.size.x, sr.size.y);
    }

    private void Update()
    {
        playY = new Vector2(sr.size.x + speedBackGround * Time.deltaTime, sr.size.y);
        sr.size = playY;
    }
}

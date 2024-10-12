using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Correndo : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    float vertical, horizontal;
    Rigidbody2D myRigidbody2d;


    void Start()
    {
        myRigidbody2d = GetComponent<Rigidbody2D>();    
    }

    void Update()
    {
        MoveCar();    
    }

    void MoveCar() 
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        myRigidbody2d.velocity = new Vector2 (horizontal * moveSpeed, vertical * moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.Instance.GameOver();
    }
}

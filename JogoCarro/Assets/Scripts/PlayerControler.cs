using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviourPun
{
    [SerializeField] float moveSpeed, moveSpeedBase, timer,timerBase;
    float vertical, horizontal;
    Rigidbody2D rb2d;
    bool controllerOn = true, coliderOn;
    void Update()
    {
        MoveCar();
        if (coliderOn)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                moveSpeed = moveSpeedBase;
                timer = timerBase;
            }
        }
    }
    [PunRPC]
    private void Initialize()
    {
        transform.Rotate(0,0,90);
        moveSpeed = moveSpeedBase;
        rb2d = GetComponent<Rigidbody2D>();
        if (!photonView.IsMine)
        {
            Color color = Color.white;
            color.a = 0.1f;
            GetComponent<SpriteRenderer>().color = color;
            controllerOn = false;

        }
    }
    void MoveCar() 
    {
        if (controllerOn) 
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            rb2d.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(controllerOn)
        {
            
            if (collision.gameObject.tag == "Muro")
            {
                timer = timerBase;
                moveSpeed = moveSpeed / 2;
                coliderOn = true;
            }
        }
        if (collision.gameObject.tag == "LinhaDeChegada")
        {
            GameManager.Instance.photonView.RPC("GameOver", RpcTarget.AllBuffered);
        }
    }
}

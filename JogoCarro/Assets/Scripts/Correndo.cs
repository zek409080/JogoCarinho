using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Correndo : MonoBehaviourPun
{
    [SerializeField] float moveSpeed, moveSpeedBase, timerColicion,timerColicionBase;
    float vertical, horizontal;
    Rigidbody2D myRigidbody2d;
    bool controllerOn = true, coliderOn, primeiroAoEncostas;

    void Update()
    {
        MoveCar();
        if (coliderOn)
        {
            timerColicion -= Time.deltaTime;
            if (timerColicion <= 0)
            {
                moveSpeed = moveSpeedBase;
                timerColicion = timerColicionBase;
            }
        }
    }

    [PunRPC]
    private void Initialize()
    {
        moveSpeed = moveSpeedBase;
        myRigidbody2d = GetComponent<Rigidbody2D>();
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
            myRigidbody2d.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(controllerOn)
        {
            
            if (collision.gameObject.tag == "Obstacle")
            {
                timerColicion = timerColicionBase;
                moveSpeed = moveSpeed / 2;
                coliderOn = true;
            }
        }
        if (collision.gameObject.tag == "LinhaDeChegada")
        {
            GameManager.Instance.photonView.RPC("FimDeJogo", RpcTarget.AllBuffered);  // GameManager.Instance.FimDeJogo();
        }
    }
}

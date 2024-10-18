using Photon.Pun; // Biblioteca para funcionalidades de rede (Photon).
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

// Classe PlayerControler gerencia o movimento e a l�gica do jogador em um ambiente multiplayer.
public class PlayerControler : MonoBehaviourPun
{
    // Vari�veis configur�veis para controle de velocidade e timers.
    [SerializeField] float moveSpeed, moveSpeedBase, timer, timerBase;
    float vertical, horizontal;
    Rigidbody2D rb2d; // Refer�ncia ao Rigidbody2D do jogador para controlar o movimento.
    bool controllerOn = true, coliderOn; // Flags para controle do jogador e detec��o de colis�o.

    // M�todo Update � chamado uma vez por frame, respons�vel pelo movimento do carro e controle de colis�es.
    void Update()
    {
        // Chama a fun��o de mover o carro.
        MoveCar();

        // Se o jogador colidiu com algo (coliderOn), reduz o timer.
        if (coliderOn)
        {
            timer -= Time.deltaTime;

            // Quando o timer chegar a zero, restaura a velocidade original.
            if (timer <= 0)
            {
                moveSpeed = moveSpeedBase;
                timer = timerBase;
            }
        }
    }

    // [PunRPC] indica que esse m�todo pode ser chamado remotamente em uma rede multiplayer via Photon.
    [PunRPC]
    private void Initialize()
    {
        // Rotaciona o objeto em 90 graus no in�cio.
        transform.Rotate(0, 0, 90);

        // Inicializa a velocidade do movimento e pega a refer�ncia do Rigidbody2D.
        moveSpeed = moveSpeedBase;
        rb2d = GetComponent<Rigidbody2D>();

        // Se o objeto n�o pertence ao jogador local, altera sua cor e desativa o controle.
        if (!photonView.IsMine)
        {
            Color color = Color.white;
            color.a = 0.1f; // Torna o objeto parcialmente transparente para outros jogadores.
            GetComponent<SpriteRenderer>().color = color;
            controllerOn = false; // Desativa o controle do objeto para jogadores que n�o s�o donos deste objeto.
        }
    }

    // Fun��o para mover o carro, usando input do teclado se o controle estiver ativo.
    void MoveCar()
    {
        if (controllerOn)
        {
            // Captura o input horizontal e vertical do jogador (teclado).
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            // Aplica o movimento ao Rigidbody2D baseado no input e na velocidade do jogador.
            rb2d.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
        }
    }

    // M�todo que detecta colis�es com outros objetos 2D.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o jogador pode controlar o objeto.
        if (controllerOn)
        {
            // Se o jogador colidir com um objeto com a tag "Muro", reduz a velocidade pela metade e inicia o timer.
            if (collision.gameObject.tag == "Muro")
            {
                timer = timerBase;
                moveSpeed = moveSpeed / 2;
                coliderOn = true; // Ativa o controle de colis�o.
            }
        }

        // Se o jogador colidir com o objeto com a tag "LinhaDeChegada", o jogo acaba.
        if (collision.gameObject.tag == "LinhaDeChegada")
        {
            // Chama o m�todo RPC "GameOver" no GameManager para todos os jogadores.
            GameManager.Instance.photonView.RPC("GameOver", RpcTarget.AllBuffered);
        }
    }
}

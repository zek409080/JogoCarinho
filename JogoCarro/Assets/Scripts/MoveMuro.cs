using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// A classe CanoMuro controla o comportamento de um objeto (como um cano) em um jogo.
public class CanoMuro : MonoBehaviour
{
    // Vari�vel privada "speed" que define a velocidade do movimento do objeto (pode ser ajustada no Inspector do Unity).
    [SerializeField] private float speed = 10f;

    // O m�todo Start() � chamado uma vez, quando o objeto � instanciado no jogo.
    private void Start()
    {
        // Rotaciona o objeto 90 graus no eixo Z, logo no in�cio.
        transform.Rotate(0, 0, 90);
    }

    // O m�todo Update() � chamado uma vez por frame, atualizando a posi��o do objeto.
    private void Update()
    {
        // Move o objeto para baixo (no eixo Y negativo) continuamente, de acordo com a velocidade definida,
        // multiplicada por Time.deltaTime para garantir que o movimento seja suave e independente da taxa de quadros (FPS).
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
}

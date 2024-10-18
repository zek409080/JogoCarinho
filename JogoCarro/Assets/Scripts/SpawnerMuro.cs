using Photon.Pun; // Importa o Photon para manipula��o de funcionalidades de rede.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe SpawnerMuro � respons�vel por instanciar (spawnar) muros em intervalos regulares de tempo.
public class SpawnerMuro : MonoBehaviour
{
    // Vari�vel configur�vel no Inspector do Unity que define o tempo base entre o spawn de muros.
    [SerializeField] private float timerBase;

    // Caminho do prefab (modelo) do muro que ser� instanciado.
    string muroPrefab = "Prefabs/Muro";

    // Vari�vel que controla o tempo restante para o pr�ximo spawn.
    private float timer;

    // O m�todo Start() � chamado uma vez no in�cio, quando o objeto � instanciado.
    private void Start()
    {
        // Verifica se o jogador � o "Master Client", ou seja, se tem permiss�o para spawnar objetos.
        if (PhotonNetwork.IsMasterClient)
        {
            // Inicializa o timer com o valor definido em timerBase e faz o primeiro spawn do muro.
            timer = timerBase;
            SpawnMuro();
        }
    }

    // O m�todo Update() � chamado uma vez por frame, gerenciando o tempo para spawnar os muros.
    private void Update()
    {
        // Somente o "Master Client" pode spawnar muros.
        if (PhotonNetwork.IsMasterClient)
        {
            // Se o timer chegou a zero, reseta o timer e spawna um novo muro.
            if (timer <= 0)
            {
                timer = timerBase;
                SpawnMuro();
            }
            // Caso contr�rio, reduz o valor do timer conforme o tempo passa.
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    // M�todo respons�vel por instanciar o muro em uma posi��o aleat�ria ao redor do objeto atual.
    private void SpawnMuro()
    {
        // Define a posi��o do spawn adicionando um valor aleat�rio no eixo X.
        Vector3 spawnPos = transform.position + new Vector3(Random.Range(-4f, 4f), 0);

        // Instancia o muro na posi��o definida e com rota��o padr�o (Quaternion.identity).
        GameObject muro = NetworkManager.instance.Instantiate(muroPrefab, spawnPos, Quaternion.identity);

        // Destroi o muro automaticamente ap�s 5 segundos.
        Destroy(muro, 5);
    }
}

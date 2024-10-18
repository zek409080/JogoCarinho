using Photon.Pun; // Importa o Photon para manipulação de funcionalidades de rede.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe SpawnerMuro é responsável por instanciar (spawnar) muros em intervalos regulares de tempo.
public class SpawnerMuro : MonoBehaviour
{
    // Variável configurável no Inspector do Unity que define o tempo base entre o spawn de muros.
    [SerializeField] private float timerBase;

    // Caminho do prefab (modelo) do muro que será instanciado.
    string muroPrefab = "Prefabs/Muro";

    // Variável que controla o tempo restante para o próximo spawn.
    private float timer;

    // O método Start() é chamado uma vez no início, quando o objeto é instanciado.
    private void Start()
    {
        // Verifica se o jogador é o "Master Client", ou seja, se tem permissão para spawnar objetos.
        if (PhotonNetwork.IsMasterClient)
        {
            // Inicializa o timer com o valor definido em timerBase e faz o primeiro spawn do muro.
            timer = timerBase;
            SpawnMuro();
        }
    }

    // O método Update() é chamado uma vez por frame, gerenciando o tempo para spawnar os muros.
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
            // Caso contrário, reduz o valor do timer conforme o tempo passa.
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    // Método responsável por instanciar o muro em uma posição aleatória ao redor do objeto atual.
    private void SpawnMuro()
    {
        // Define a posição do spawn adicionando um valor aleatório no eixo X.
        Vector3 spawnPos = transform.position + new Vector3(Random.Range(-4f, 4f), 0);

        // Instancia o muro na posição definida e com rotação padrão (Quaternion.identity).
        GameObject muro = NetworkManager.instance.Instantiate(muroPrefab, spawnPos, Quaternion.identity);

        // Destroi o muro automaticamente após 5 segundos.
        Destroy(muro, 5);
    }
}

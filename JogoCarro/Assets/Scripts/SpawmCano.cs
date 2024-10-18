using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanoSpawner : MonoBehaviour
{


    [SerializeField] private float timerBase;
    private float heightRange = 4f;
    string obstaclePrefab = "Prefabs/Caixa";
    private float timer;
    private void Start()
    {
        if (PhotonNetwork.IsMasterClient )
        {
            SpawnCano();
            timer = timerBase;
        }
    }

    private void Update()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            if (timer <= 0)
            {
                timer = timerBase;
                SpawnCano();
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    private void SpawnCano() 
    {
        Vector3 spawnPos = transform.position + new Vector3(Random.Range(-heightRange, heightRange),0);
        GameObject cano = NetworkManager.instance.Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
        Destroy(cano, 10);
    }
}

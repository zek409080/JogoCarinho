using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMuro : MonoBehaviour
{
    [SerializeField] private float timerBase;
    string muroPrefab = "Prefabs/Muro";
    private float timer;
    private void Start()
    {
        if (PhotonNetwork.IsMasterClient )
        {
            timer = timerBase;
            SpawnMuro();     
        }
    }

    private void Update()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            if (timer <= 0)
            {
                timer = timerBase;
                SpawnMuro();
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    private void SpawnMuro() 
    {
        Vector3 spawnPos = transform.position + new Vector3(Random.Range(-4f, 4f),0);
        GameObject muro = NetworkManager.instance.Instantiate(muroPrefab, spawnPos, Quaternion.identity);
        Destroy(muro, 5);
    }
}

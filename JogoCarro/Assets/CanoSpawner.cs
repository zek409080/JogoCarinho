using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanoSpawner : MonoBehaviour
{
    [SerializeField] private float maxTime = 1.5f;
    [SerializeField] private float heightRange = 0.45f;
    [SerializeField] private GameObject _cano;

    private float timer;

    private void Start()
    {
        SpawnCano();
    }

    private void Update()
    {
        if (timer > maxTime) 
        {
            SpawnCano();
            timer = 0;
        }
        timer += Time.deltaTime;
    }

    private void SpawnCano() 
    {
        Vector3 spawnPos = transform.position + new Vector3(0, Random.Range(-heightRange, heightRange));
        GameObject cano = Instantiate(_cano, spawnPos, Quaternion.identity);

        Destroy(cano, 10f);
    }
}

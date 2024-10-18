using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class CanoMuro : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private void Start()
    {
        transform.Rotate(0, 0, 90);
    }
    private void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
}

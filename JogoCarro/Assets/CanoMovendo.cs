using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class CanoMovendo : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}

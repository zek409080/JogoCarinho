using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinhaDeChegada : MonoBehaviour
{

     public GameObject linhaDeChegada;

    void Start()
    {
        linhaDeChegada.SetActive(false);

        StartCoroutine(AtivarLinhaDeChegadaDepoisDe30Segundos());
    }

    IEnumerator AtivarLinhaDeChegadaDepoisDe30Segundos()
    {
        yield return new WaitForSeconds(3f);

        linhaDeChegada.SetActive(true);
    }
}

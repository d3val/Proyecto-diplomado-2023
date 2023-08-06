using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverCamara : MonoBehaviour
{
    [SerializeField] private GameObject jugador = null;
    [SerializeField] private Vector3 offset = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        transform.position = jugador.transform.position + offset;
    }
}

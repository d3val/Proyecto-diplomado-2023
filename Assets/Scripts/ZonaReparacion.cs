using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaReparacion : MonoBehaviour
{
    public float condicion = 100;
    public bool estaDanado = false;
    // Start is called before the first frame update
    void Start()
    {
        estaDanado = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (condicion > 100)
        {
            condicion = 100;
            Debug.Log("Atraccion reparada");
        }
        if (condicion < 0)
        {
            Debug.Log("Atraccion dañada");
            return;
        }
        if (estaDanado)
        {
            condicion -= Time.deltaTime;
            Debug.Log("La condicion es del: " + condicion);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        other.GetComponent<Jugador>().zonaReparacionActual = this;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        other.GetComponent<Jugador>().zonaReparacionActual = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    [SerializeField] float velocidadReparacion = 1f;
    public ZonaReparacion zonaReparacionActual;

    public Comida comidaActual = null;
    // Start is called before the first frame update
    void Start()
    {
        zonaReparacionActual = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Reparar();
        }

        if (comidaActual == null)
        {
            Debug.Log("No hay comida");
        }
        else
        {
            Debug.Log("Tengo comida que recupera: "+ comidaActual.energia+
                "\nY me tardare "+comidaActual.tiempoDeConsumo+" en terminarlo");
        }
    }

    private void Reparar()
    {
        if (zonaReparacionActual == null)
            return;
        if (zonaReparacionActual.condicion >= 100)
            return;
        zonaReparacionActual.estaDanado = false;
        zonaReparacionActual.condicion += velocidadReparacion * Time.deltaTime;
    }
}

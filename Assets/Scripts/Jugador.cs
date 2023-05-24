using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    [SerializeField] float velocidadReparacion = 1f;
    public ZonaReparacion zonaReparacionActual;

    public Comida comidaActual = null;

    private ZonaComida zonaComidaActual;

    //Variables animacion
    [SerializeField] Animator animator;
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
            Debug.Log("Tengo comida que recupera: " + comidaActual.energia +
                "\nY me tardare " + comidaActual.tiempoDeConsumo + " en terminarlo");
        }

        AccionZonaComida();
    }

    private void AccionZonaComida()
    {
        if (zonaComidaActual == null)
            return;

        switch (zonaComidaActual.estado)
        {
            //Cuando se va a realizar la orden
            case 1:
                if (Input.GetKey(KeyCode.E))
                {
                    zonaComidaActual.estado = 2;
                    animator.SetTrigger("trigger_ordenar");
                }
                break;
            // Cuando se va a recoger la orden
            case 3:
                if (Input.GetKey(KeyCode.E) && comidaActual == null)
                {
                    comidaActual = zonaComidaActual.comidaServida;
                    zonaComidaActual.estado = 4;
                }
                break;

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

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Zona comida"))
            return;

        zonaComidaActual = other.GetComponent<ZonaComida>();

        if (zonaComidaActual.estado == 0)
            zonaComidaActual.estado = 1;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Zona comida"))
            return;

        if (zonaComidaActual.estado == 1)
            zonaComidaActual.estado = 0;

        UIGameManager.instance.DesactivarMensajeAccion();
        zonaComidaActual = null;
    }
}

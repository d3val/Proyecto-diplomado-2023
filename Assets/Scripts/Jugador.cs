using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    [SerializeField] float velocidadReparacion = 1f;
    public ZonaReparacion zonaReparacionActual;

    public Comida comidaActual = null;

    private ZonaComida zonaComidaActual;

    private MovimientoJugador movimientoJugador;

    //Variables animacion
    [SerializeField] Animator animator;

    [Header("Herramientas del jugador")]
    [SerializeField] GameObject martillo;


    // Start is called before the first frame update
    void Start()
    {
        zonaReparacionActual = null;
        zonaComidaActual = null;
        movimientoJugador = GetComponent<MovimientoJugador>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.juegoPausado)
            return;
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
        AccionZonaReparacion();
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
    private void AccionZonaReparacion()
    {
        if (zonaReparacionActual == null) return;

        switch (zonaReparacionActual.estado)
        {
            case 1:
                if (Input.GetKey(KeyCode.E))
                {
                    zonaReparacionActual.estado = 2;
                    martillo.SetActive(true);
                    animator.SetTrigger("trigger_reparando");
                    movimientoJugador.FrenarMovimiento();
                    movimientoJugador.enabled = false;
                }
                break;
            case 3:
                animator.SetTrigger("trigger_reparadoFinalizado");
                martillo.SetActive(false);
                movimientoJugador.enabled = true;
                zonaReparacionActual = null;
                break;
            default:
                break;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zona comida"))
        {
            zonaComidaActual = other.GetComponent<ZonaComida>();
            if (zonaComidaActual.estado == 0)
                zonaComidaActual.estado = 1;
            return;

        }
        if (other.gameObject.CompareTag("Zona reparacion"))
        {
            zonaReparacionActual = other.GetComponent<ZonaReparacion>();
            return;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Zona comida"))
        {
            if (zonaComidaActual.estado == 1)
                zonaComidaActual.estado = 0;
            //UILevelManager.instance.DesactivarMensajeAccion();
            zonaComidaActual = null;
            return;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public ZonaReparacion zonaReparacionActual;

    public Comida comidaActual = null;

    private ZonaComida zonaComidaActual;
    private ZonaComedero zonaComederoActual;
    private bool comiendo;

    private MovimientoJugador movimientoJugador;

    //Variables animacion
    [SerializeField] Animator animator;

    [Header("Herramientas del jugador")]
    [SerializeField] GameObject martillo;
    [SerializeField] GameObject comida;
    [SerializeField] int goldKeys = 5;

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
        if (LevelManager.juegoPausado)
            return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            AccionZonaComida();
            AccionZonaReparacion();
            AccionZonaComedero();
        }
    }

    private void AccionZonaComida()
    {
        if (zonaComidaActual == null)
            return;

        switch (zonaComidaActual.estado)
        {
            //Cuando se va a realizar la orden
            case 1:

                zonaComidaActual.estado = 2;
                animator.SetTrigger("trigger_ordenar");

                break;
            // Cuando se va a recoger la orden
            case 3:
                if (comidaActual == null)
                {
                    comidaActual = zonaComidaActual.comidaServida;
                    UILevelManager.instance.SetImagenComida(comidaActual.sprite);
                    UILevelManager.instance.SetActiveMensajeAccion(false);
                    zonaComidaActual.estado = 4;
                }
                break;

        }
    }

    IEnumerator Reparando()
    {
        martillo.SetActive(true);
        animator.SetTrigger("trigger_reparando");
        movimientoJugador.FrenarMovimiento();
        movimientoJugador.enabled = false;
        while (zonaReparacionActual.estado == 2)
        {

            yield return null;
        }
        animator.SetTrigger("trigger_reparadoFinalizado");
        martillo.SetActive(false);
        movimientoJugador.enabled = true;
        zonaReparacionActual = null;
    }

    private void AccionZonaReparacion()
    {
        if (zonaReparacionActual == null) return;

        switch (zonaReparacionActual.estado)
        {
            case 1:
                zonaReparacionActual.estado = 2;
                StopAllCoroutines();
                StartCoroutine(zonaReparacionActual.Reparar());
                StartCoroutine(Reparando());

                break;
            case 4:
                if (goldKeys > 0)
                {
                    zonaReparacionActual.estado = 2;
                    goldKeys--;
                    UILevelManager.instance.RemoveWrench();
                    StopAllCoroutines();
                    StartCoroutine(zonaReparacionActual.Reparar());
                    StartCoroutine(Reparando());
                }
                break;
            default:
                break;
        }
    }

    private void AccionZonaComedero()
    {
        if (zonaComederoActual == null)
            return;

        if (Input.GetKeyDown(KeyCode.E) && comidaActual != null && !comiendo)
        {
            UILevelManager.instance.SetActiveMensajeAccion(false);
            StartCoroutine(Comer());
        }
    }

    IEnumerator Comer()
    {
        comiendo = true;
        comida.SetActive(true);
        movimientoJugador.EmpezarAComer();
        zonaComederoActual.IniciarEspera(comidaActual.tiempoDeConsumo);
        transform.SetPositionAndRotation(zonaComederoActual.wayPoint.position, zonaComederoActual.wayPoint.rotation);
        yield return new WaitForSeconds(comidaActual.tiempoDeConsumo);
        UILevelManager.instance.LimpiarImagenComida();
        transform.SetLocalPositionAndRotation(zonaComederoActual.endPoint.position, zonaComederoActual.endPoint.rotation);
        movimientoJugador.ReanudarFisicas();
        movimientoJugador.RecuperarEstamina(comidaActual.energia);
        comida.SetActive(false);
        comidaActual = null;
        comiendo = false;
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
        if (other.gameObject.CompareTag("Zona comedero"))
        {
            zonaComederoActual = other.GetComponent<ZonaComedero>();
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

        if (other.gameObject.CompareTag("Zona comedero"))
        {
            zonaComederoActual = null;
            return;
        }

        movimientoJugador.enabled = true;
        animator.Play("Movimiento.Idle");
        StopAllCoroutines();

    }
}

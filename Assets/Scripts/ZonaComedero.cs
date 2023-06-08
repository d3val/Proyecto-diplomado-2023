using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaComedero : MonoBehaviour
{
    public Transform wayPoint;
    public Transform endPoint;
    private Jugador jugador;
    private float tiempoEspera;
    private float timer;
    private bool jugadorCerca;
    private UIZona UIZona;
    public int estado;
    // Start is called before the first frame update
    void Start()
    {
        UIZona = GetComponent<UIZona>();
    }

    private void Update()
    {
        if (estado == 0)
        {
            UIZona.DesactivarUI();
            return;
        }

        UIZona.ActivarUI();
        timer += Time.deltaTime;
        UIZona.ActualizarSlider(timer);
        if (timer >= tiempoEspera)
        {
            estado = 0;
            UIZona.DesactivarUI();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        jugador = other.GetComponent<Jugador>();
        if (jugador.comidaActual == null)
            return;

        UILevelManager.instance.SetMensajeAccion("Comer");
    }

    public void IniciarEspera(float tiempo)
    {
        tiempoEspera = tiempo;
        UIZona.slider.maxValue = tiempoEspera;
        timer = 0;
        estado = 1;
        UIZona.ActualizarLabel("Comiendo...");
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        jugador = null;
        UILevelManager.instance.SetActiveMensajeAccion(false);
    }
}

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
    private UIZona UIZona;
    // Start is called before the first frame update
    void Start()
    {
        UIZona = GetComponent<UIZona>();
    }
public IEnumerator IniciarEspera(float tiempo)
    {
        tiempoEspera = tiempo;
        UIZona.slider.maxValue = tiempoEspera;
        timer = 0;
        UIZona.ActivarUI();
        UIZona.ActualizarLabel("Comiendo...");
        while (timer < tiempoEspera)
        {
            timer+= Time.deltaTime;
            UIZona.ActualizarSlider(timer);
            yield return null;
        }
        UIZona.DesactivarUI();
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

    

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        jugador = null;
        UILevelManager.instance.SetActiveMensajeAccion(false);
        UIZona.DesactivarUI();
    }
}

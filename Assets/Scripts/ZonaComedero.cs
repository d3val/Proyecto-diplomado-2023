using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaComedero : MonoBehaviour
{
    public Transform wayPoint;
    private bool jugadorCerca;
    private UIZona UIZona;
    public int estado;
    // Start is called before the first frame update
    void Start()
    {
        UIZona= GetComponent<UIZona>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        Jugador aux= other.GetComponent<Jugador>();
        if (aux.comidaActual == null)
            return;

        UILevelManager.instance.SetMensajeAccion("Comer");
        Debug.Log("Comer");
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        UILevelManager.instance.SetActiveMensajeAccion(false);
    }
}

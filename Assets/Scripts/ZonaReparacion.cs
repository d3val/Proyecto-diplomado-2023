using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaReparacion : MonoBehaviour
{
    [Header("Configuración zona reparación")]
    [SerializeField] float condicion = 100;
    [SerializeField] float tiempoInmunidad;
    [SerializeField] float velocidadReparacion;
    [SerializeField] float velocidadDeterioro;
    [HideInInspector]
    public int estado;

    private float timer;

    private bool jugadorCerca;
    private UIZona UIZona;
    // Start is called before the first frame update
    void Start()
    {
        UIZona = GetComponent<UIZona>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.juegoPausado)
            return;
        RevisarEstado();
    }

    private void RevisarEstado()
    {
        switch (estado)
        {
            case 0:
                UIZona.DesactivarUI();
                if (!UIGameManager.instance.enZona)
                    UIGameManager.instance.DesactivarMensajeAccion();
                break;
            case 1:
                UIZona.ActivarUI();
                condicion -= Time.deltaTime * velocidadDeterioro;
                UIZona.ActualizarSlider(condicion);
                UIZona.ActualizarLabel("!!!!!!");
                if (jugadorCerca)
                {
                    if (UIGameManager.instance.enZona)
                        UIGameManager.instance.SetMensajeAccion("Reparar");
                }

                if (condicion < 0)
                    estado = 4;

                break;
            case 2:
                UIZona.ActivarUI();
                UIZona.ActualizarLabel("Reparando");
                UIGameManager.instance.DesactivarMensajeAccion();
                if (condicion < 100)
                {
                    condicion += Time.deltaTime * velocidadReparacion;
                    UIZona.ActualizarSlider(condicion);
                }
                else
                {
                    estado = 3;
                }
                break;
            case 3:
                UIZona.ActualizarLabel("Inmune");
                if (timer < tiempoInmunidad)
                    timer += Time.deltaTime;
                else
                {
                    timer = 0;
                    estado = 0;
                }
                break;

        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        jugadorCerca = true;
        UIGameManager.instance.enZona = true;
        Debug.Log(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        jugadorCerca = false;
        UIGameManager.instance.enZona = false;
        UIGameManager.instance.DesactivarMensajeAccion();
    }
}

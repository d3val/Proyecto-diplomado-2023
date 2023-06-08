using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaReparacion : MonoBehaviour
{
    [Header("Configuración zona reparación")]
    public float condicion = 100;
    [SerializeField] float tiempoInmunidad;
    [SerializeField] float velocidadReparacion;
    [SerializeField] float velocidadDeterioro;
    //[HideInInspector]
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
                /* if (!GameManager.jugadorEnZona)
                     UILevelManager.instance.SetActiveMensajeAccion(false);*/
                break;
            case 1:
                UIZona.ActivarUI();
                condicion -= Time.deltaTime * velocidadDeterioro;
                UIZona.ActualizarSlider(condicion);
                UIZona.ActualizarLabel("!!!!!!");
                if (jugadorCerca)
                {
                    if (GameManager.jugadorEnZona)
                        UILevelManager.instance.SetMensajeAccion("Reparar");
                }

                if (condicion < 0)
                    estado = 4;

                break;
            case 2:
                UIZona.ActivarUI();
                UIZona.ActualizarLabel("Reparando");
                UILevelManager.instance.SetActiveMensajeAccion(false);
                if (condicion < 100)
                {
                    condicion += Time.deltaTime * velocidadReparacion;
                    UIZona.ActualizarSlider(condicion);
                }
                else
                    StartCoroutine(Recover());
                break;
            case 3:
                UIZona.ActualizarLabel("Inmune");
                break;
            case 4:
                UIZona.ActualizarLabel("Fuera de servicio");
                break;
            default:
                Debug.Log("Este mensaje no debería aparecer");
                break;

        }
    }

    IEnumerator Recover()
    {
        estado = 3;
        yield return new WaitForSeconds(tiempoInmunidad);
        estado = 0;
    }

    public void Fallar()
    {
        if (estado != 0)
            return;
        estado = 1;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        jugadorCerca = true;
        GameManager.jugadorEnZona = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        jugadorCerca = false;
        GameManager.jugadorEnZona = false;
        UILevelManager.instance.SetActiveMensajeAccion(false);
    }
}

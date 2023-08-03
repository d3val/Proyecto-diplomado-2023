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
    [SerializeField] ParticleSystem humo;
    [SerializeField] List<ParticleSystem> chispas = new List<ParticleSystem>();
    //[HideInInspector]
    public int estado;

    private float timer;

    private bool jugadorCerca;
    private UIZona UIZona;
    public bool isFunctional = true;
    // Start is called before the first frame update
    void Start()
    {
        UIZona = GetComponent<UIZona>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.juegoPausado)
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
                SetParticlesActive(true);
                if (jugadorCerca)
                {
                    if (LevelManager.jugadorEnZona)
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
                UIZona.ActualizarLabel("Listo!");
                break;
            case 4:
                UIZona.ActualizarLabel("Fuera de servicio");
                isFunctional = false;
                if (jugadorCerca)
                {
                    if (LevelManager.jugadorEnZona)
                        UILevelManager.instance.SetMensajeAccion("Usar llave");
                }
                break;
            default:
                Debug.Log("Este mensaje no debería aparecer");
                break;

        }
    }

    private void SetParticlesActive(bool active)
    {
        if (active)
        {
            if (!humo.isPlaying)
                humo.Play();

            foreach (var item in chispas)
            {
                if (!item.isPlaying)
                    item.Play();
            }
        }
        else
        {
            humo.Stop();
            foreach (var item in chispas)
            {
                item.Stop();
            }
        }
    }
    IEnumerator Recover()
    {
        estado = 3;
        SetParticlesActive(false);
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
        LevelManager.jugadorEnZona = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        jugadorCerca = false;
        LevelManager.jugadorEnZona = false;
        UILevelManager.instance.SetActiveMensajeAccion(false);
        other.GetComponent<Jugador>().zonaReparacionActual = null;
    }
}

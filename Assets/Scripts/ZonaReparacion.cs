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

    public bool jugadorCerca;
    private UIZona UIZona;
    public bool isFunctional = true;
    const int REPOSO = 0, DANADO = 1, REPARANDO = 2, LISTO = 3, INUTIL = 4;

    // Start is called before the first frame update
    void Start()
    {
        UIZona = GetComponent<UIZona>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.juegoPausado)
            return;
        RevisarEstado();
    }

    public IEnumerator Reparar()
    {
        UIZona.ActivarUI();
        UIZona.ActualizarLabel("Reparando");
        UILevelManager.instance.SetActiveMensajeAccion(false);


        while (condicion < 100)
        {
            Debug.Log(condicion);
            yield return null;
            if (!jugadorCerca)
            {
                estado = 1;
                Debug.Log("Se fue");
                StopCoroutine(Reparar());
            }
            condicion += Time.deltaTime * velocidadReparacion;
            UIZona.ActualizarSlider(condicion);
        }
        StartCoroutine(Recover());
    }
    private void RevisarEstado()
    {
        switch (estado)
        {
            case REPOSO:
                UIZona.DesactivarUI();
                /* if (!GameManager.jugadorEnZona)
                     UILevelManager.instance.SetActiveMensajeAccion(false);*/
                break;
            case DANADO:


                break;
            case REPARANDO:

                break;
            case LISTO:
                UIZona.ActualizarLabel("Listo!");
                break;
            case INUTIL:
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

    public IEnumerator Danar()
    {
        while (estado == DANADO)
        {
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

            yield return null;
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
        if (estado != REPOSO)
            return;
        estado = DANADO;
        StartCoroutine(Danar());
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

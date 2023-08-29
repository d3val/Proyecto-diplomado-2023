using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaReparacion : MonoBehaviour
{
    
    public float condicion { get; private set; }
    [Header("Configuración zona reparación")]
    [SerializeField] float tiempoInmunidad;
    [SerializeField] float velocidadReparacion;
    [SerializeField] float velocidadDeterioro;
    [SerializeField] ParticleSystem humo;
    [SerializeField] List<ParticleSystem> chispas = new List<ParticleSystem>();
    //[HideInInspector]
    public int estado { private set; get; }

    public bool jugadorCerca;
    private UIZona UIZona;
    public bool isFunctional = true;
    const int REPOSO = 0, DANADO = 1, REPARANDO = 2, LISTO = 3, INUTIL = 4;

    // Start is called before the first frame update
    void Start()
    {
        condicion = 100;
        UIZona = GetComponent<UIZona>();
    }

    public IEnumerator Reparar()
    {
        estado = REPARANDO;
        UIZona.ActivarUI();
        UIZona.ActualizarLabel("Reparando");
        UILevelManager.instance.SetActiveMensajeAccion(false);


        while (condicion < 100)
        {
            yield return null;

            condicion += Time.deltaTime * velocidadReparacion;
            UIZona.ActualizarSlider(condicion);
        }
        StartCoroutine(Recover());
    }

    public IEnumerator Danar()
    {
        estado = DANADO;
        while (estado == DANADO)
        {
            UIZona.ActivarUI();
            condicion -= Time.deltaTime * velocidadDeterioro;
            UIZona.ActualizarSlider(condicion);
            UIZona.ActualizarLabel("!!!!!!");
            SetParticlesActive(true);
            if (jugadorCerca)
            {
                UILevelManager.instance.SetMensajeAccion("Reparar");
            }

            if (condicion < 0)
            {
                UIZona.ActualizarLabel("Fuera de servicio");
                isFunctional = false;
                estado = 4;
            }
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
        UIZona.ActualizarLabel("Listo!");
        estado = LISTO;
        SetParticlesActive(false);
        yield return new WaitForSeconds(tiempoInmunidad);
        estado = REPOSO;
        UIZona.DesactivarUI();
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

        if (estado == INUTIL)
        {
            UILevelManager.instance.SetMensajeAccion("Usar llave");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        jugadorCerca = false;
        UILevelManager.instance.SetActiveMensajeAccion(false);
        if (estado == REPARANDO)
        {
            StopAllCoroutines();
            StartCoroutine(Danar());
        }
    }
}

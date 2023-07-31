using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtraccionesManager : MonoBehaviour
{
    public static AtraccionesManager Instance;
    public List<ZonaReparacion> zonasReparacion;
    [SerializeField] float intervaloFallos = 5f;
    [SerializeField] float inicioFallos = 3f;
    public int zonasFuncionales;
    public List<Atraccion> atracciones;
    public List<Atraccion> atraccionesVisitantes;
    List<float> status = new List<float>();
    public static int atraccionesRotas { private set; get; }
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        GameObject[] zonas = GameObject.FindGameObjectsWithTag("Zona reparacion");
        foreach (GameObject zona in zonas)
        {
            zonasReparacion.Add(zona.GetComponent<ZonaReparacion>());
        }
        zonasFuncionales = zonasReparacion.Count;

        foreach (Atraccion atraccion in atracciones)
        {
            if (atraccion.visitorInteractable)
                atraccionesVisitantes.Add(atraccion);
        }
        atraccionesRotas = 0;
        InvokeRepeating("IniciarFallo", inicioFallos, intervaloFallos);
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.juegoPausado)
            return;
        ActualizarStatus();
    }

    private void RevisarZonas()
    {
        zonasFuncionales = zonasReparacion.Count;
        //List<ZonaReparacion> zonasInutiles = new List<ZonaReparacion>();
        foreach (ZonaReparacion zona in zonasReparacion)
        {
            if (!zona.isFunctional)
            {
                zonasFuncionales--;
            }
        }
        /*foreach (ZonaReparacion zonaInutil in zonasInutiles)
        {
            zonasFuncionales.Remove(zonaInutil);
        }*/
    }

    private void IniciarFallo()
    {
        // RevisarZonas();
        if (zonasFuncionales <= 0)
            return;
        int i = Random.Range(0, zonasReparacion.Count);
        zonasReparacion[i].Fallar();
    }

    private void ActualizarStatus()
    {
        atraccionesRotas = 0;
        status.Clear();
        foreach (Atraccion atraccion in atracciones)
        {
            status.Add(atraccion.PromediarCondicion());

            if (atraccion.condicionGeneral <= 0)
            {
                atraccion.CerrarAtraccion();
                atraccionesRotas++;
            }
            else
            {
                atraccion.ReabrirAtraccion();
            }
        }
        UILevelManager.instance.AtualizarCondiciones(status);

        if (atraccionesRotas >= LevelManager.Instance.limiteAtraccionesRotas)
        {
            LevelManager.Instance.GameOver();
        }
    }

    public List<float> GetCondiciones()
    {
        List<float> ret = new List<float>();
        foreach (var item in atracciones)
        {
            ret.Add(item.condicionGeneral);
        }
        return ret;
    }

}

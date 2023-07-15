using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtraccionesManager : MonoBehaviour
{
    public List<ZonaReparacion> zonasReparacion;
    [SerializeField] float intervaloFallos = 5f;
    [SerializeField] float inicioFallos = 3f;
    public List<ZonaReparacion> zonasFuncionales;
    public List<Atraccion> atracciones;
    public List<Atraccion> atraccionesVisitantes;
    List<float> status = new List<float>();
    public static int atraccionesRotas { private set; get; }
    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] zonas = GameObject.FindGameObjectsWithTag("Zona reparacion");
        foreach (GameObject zona in zonas)
        {
            zonasReparacion.Add(zona.GetComponent<ZonaReparacion>());
        }
        zonasFuncionales = zonasReparacion;

        zonas = GameObject.FindGameObjectsWithTag("Instalacion");
        foreach (GameObject atraccion in zonas)
        {
            Atraccion aux = atraccion.GetComponent<Atraccion>();
            atracciones.Add(aux);
            if (aux.visitorInteractable)
                atraccionesVisitantes.Add(aux);
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
        List<ZonaReparacion> zonasInutiles = new List<ZonaReparacion>();
        foreach (ZonaReparacion zona in zonasFuncionales)
        {
            if (zona.estado == 4)
            {
                zonasInutiles.Add(zona);
            }
        }
        foreach (ZonaReparacion zonaInutil in zonasInutiles)
        {
            zonasFuncionales.Remove(zonaInutil);
        }
    }

    private void IniciarFallo()
    {
        RevisarZonas();
        if (zonasFuncionales.Count <= 0)
            return;
        int i = Random.Range(0, zonasFuncionales.Count);
        zonasFuncionales[i].Fallar();
    }

    private void ActualizarStatus()
    {
        atraccionesRotas = 0;
        status.Clear();
        foreach (Atraccion atraccion in atracciones)
        {
            status.Add(atraccion.PromediarCondicion());

            if (atraccion.condicionGeneral <= 0)
                atraccionesRotas++;

        }
        UILevelManager.instance.AtualizarCondiciones(status);

        if (atraccionesRotas >= LevelManager.Instance.limiteAtraccionesRotas)
        {
            LevelManager.Instance.GameOver();
        }
    }
}

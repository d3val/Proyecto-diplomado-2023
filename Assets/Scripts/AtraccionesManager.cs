using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtraccionesManager : MonoBehaviour
{
    public List<ZonaReparacion> zonasReparacion;
    [SerializeField] float intervaloFallos = 5f;
    [SerializeField] float inicioFallos = 3f;
    public List<ZonaReparacion> zonasFuncionales;
    // Start is called before the first frame update
    void Start()
    {

        GameObject[] zonas = GameObject.FindGameObjectsWithTag("Zona reparacion");
        foreach (GameObject zona in zonas)
        {
            zonasReparacion.Add(zona.GetComponent<ZonaReparacion>());
        }
        zonasFuncionales = zonasReparacion;
        InvokeRepeating("IniciarFallo", inicioFallos, intervaloFallos);
    }

    // Update is called once per frame
    void Update()
    {
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
        foreach(ZonaReparacion zonaInutil in zonasInutiles)
        {
            zonasFuncionales.Remove(zonaInutil);
        }
    }

    private void IniciarFallo()
    {
       

        RevisarZonas();
        Debug.Log(zonasFuncionales.Count);
        if (zonasFuncionales.Count <= 0)
            return;
        int i = Random.Range(0, zonasFuncionales.Count);
        zonasFuncionales[i].Fallar();
    }
}

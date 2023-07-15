using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class Atraccion : MonoBehaviour
{
    ZonaReparacion[] list;
    public bool visitorInteractable = true;
    public GameObject destinationPoint;
    public List<Transform> puntosAnclaje = new List<Transform>();
    int indexAnclaje = 0;
    public float condicionGeneral { private set; get; }/*{ private set; get; }*/
    // Start is called before the first frame update
    void Start()
    {
        list = GetComponentsInChildren<ZonaReparacion>();
        indexAnclaje = 0;
    }

    public float PromediarCondicion()
    {
        float promedio = 0;
        foreach (ZonaReparacion zona in list)
        {
            promedio += zona.condicion;
        }
        condicionGeneral = promedio / list.Length;
        return condicionGeneral;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Visitante"))
            return;

        Visitante visitante = other.GetComponent<Visitante>();
        if (indexAnclaje >= puntosAnclaje.Count)
            indexAnclaje = 0;
        if (visitante.currentAtraction == this)
        {
            SubirVisitante(visitante);
        }
    }

    protected virtual void SubirVisitante(Visitante vis)
    {
        vis.DisableAgent();
        vis.Subir(puntosAnclaje[indexAnclaje]);
        indexAnclaje++;
    }

    protected virtual void Ronda()
    {
    }
}

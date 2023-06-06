using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class Atraccion : MonoBehaviour
{
    ZonaReparacion[] list;
    public float condicionGeneral { private set; get; }/*{ private set; get; }*/
    // Start is called before the first frame update
    void Start()
    {
        list = GetComponentsInChildren<ZonaReparacion>();
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
}

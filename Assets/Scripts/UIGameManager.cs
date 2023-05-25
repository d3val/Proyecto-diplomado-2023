using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class UIGameManager : MonoBehaviour
{
    [Header("UI Acciones del jugador")]
    [SerializeField] GameObject mensajeAccion;
    [SerializeField] TextMeshProUGUI textoAccion;

    public static UIGameManager instance;
    public bool enZona;

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);

        instance = this;
    }

    public void SetMensajeAccion(string mensaje)
    {
        mensajeAccion.SetActive(true);
        textoAccion.text = mensaje;
        Debug.Log("Se activo");
    }

    public void DesactivarMensajeAccion()
    {
        mensajeAccion.SetActive(false);
        Debug.Log("Se desactivo");
    }
}

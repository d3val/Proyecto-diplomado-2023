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
    }

    public void DesactivarMensajeAccion()
    {
        mensajeAccion.SetActive(false);
    }
}

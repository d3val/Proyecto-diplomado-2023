using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILevelManager : MonoBehaviour
{
    [SerializeField] GameObject panelJuego;
    [SerializeField] GameObject panelPausa;
    [SerializeField] GameObject panelFin;
    [SerializeField] TextMeshProUGUI contadorTiempo;
    [SerializeField] Image imagenComida;
    [SerializeField] List<TextMeshProUGUI> condicionAtracciones;

    private int minutos;
    private int segundos;

    [Header("UI Acciones del jugador")]
    [SerializeField] GameObject mensajeAccion;
    [SerializeField] TextMeshProUGUI textoAccion;

    public static UILevelManager instance;
    public bool enZona;

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);

        instance = this;
    }

    private void Update()
    {
        if (GameManager.juegoPausado)
            return;

        ActualizarTimerUI();
    }

    private void ActualizarTimerUI()
    {
        minutos = (int)(GameManager.Instance.tiempoNivel / 60);
        segundos = (int)(GameManager.Instance.tiempoNivel - minutos * 60);
        contadorTiempo.text = String.Format("{0:00}:{1:00}", minutos, segundos);
    }

    public void SetImagenComida(Sprite spriteComida)
    {
        imagenComida.color = new Color(255, 255, 255, 1);
        imagenComida.sprite = spriteComida;
    }

    public void SetMensajeAccion(string mensaje)
    {
        SetActiveMensajeAccion(true);
        textoAccion.text = mensaje;
         Debug.Log("Se activo");
    }

    public void SetActiveMensajeAccion(bool estado)
    {
        mensajeAccion.SetActive(estado);
        Debug.Log("Se desactivo");

    }

    public void SetActivePanelPausa(bool estado)
    {
        panelPausa.SetActive(estado);
    }

    public void SetActivePanelJuego(bool estado)
    {
        panelJuego.SetActive(estado);
    }

    public void SetActivePanelFinJuego(bool estado)
    {
        panelFin.SetActive(estado);
    }

    public void AtualizarCondiciones(List<float> valores)
    {
        for (int i = 0; i < condicionAtracciones.Count; i++)
        {
            condicionAtracciones[i].text = string.Format("{0}%", (int)valores[i]);
        }
    }
}

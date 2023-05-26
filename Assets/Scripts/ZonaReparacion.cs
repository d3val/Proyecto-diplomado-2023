using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ZonaReparacion : MonoBehaviour
{
    public float condicion = 100;

    public int estado;
    public float tiempoInmune;
    private float timer;
    public float velocidadReparacion;
    public float velocidadDeterioro;
    [SerializeField] GameObject sliderCondicion;
    Slider slider;
    public Image sliderFill;
    private bool jugadorCerca;
    public bool estaDanado;
    public GameObject textoAccion;
    private TextMeshProUGUI texto;
    // Start is called before the first frame update
    void Start()
    {
        slider = sliderCondicion.GetComponent<Slider>();
        texto = textoAccion.GetComponent<TextMeshProUGUI>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.juegoPausado)
            return;
        RevisarEstado();
    }
    private void ActualizarUI()
    {
        slider.value = condicion;
        if (slider.value < slider.maxValue / 3)
            sliderFill.color = Color.red;
        else if (slider.value < slider.maxValue * 2 / 3)
            sliderFill.color = Color.yellow;
        else
            sliderFill.color = Color.green;
    }
    private void RevisarEstado()
    {
        switch (estado)
        {
            case 0:
                sliderCondicion.SetActive(false);
                if (!UIGameManager.instance.enZona)
                    UIGameManager.instance.DesactivarMensajeAccion();
                break;
            case 1:
                sliderCondicion.SetActive(true);
                if (jugadorCerca)
                {
                    if (UIGameManager.instance.enZona)
                        UIGameManager.instance.SetMensajeAccion("Reparar");
                }
                
                condicion -= Time.deltaTime * velocidadDeterioro;
                ActualizarUI();

                if (condicion < 0)
                {
                    estado = 4;
                }
                break;
            case 2:
                textoAccion.SetActive(true);
                texto.text = "Reparando";
                UIGameManager.instance.DesactivarMensajeAccion();
                if (condicion < 100)
                {
                    condicion += Time.deltaTime * velocidadReparacion;
                    ActualizarUI();
                }
                else
                {
                    estado = 3;
                }
                break;
            case 3:
                texto.text = "Inmune";
                if (timer < tiempoInmune)
                    timer += Time.deltaTime;
                else
                {
                    timer = 0;
                    estado = 0;
                    textoAccion.SetActive(false);
                }
                break;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        jugadorCerca = true;
        UIGameManager.instance.enZona = true;
        Debug.Log(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        jugadorCerca = false;
        UIGameManager.instance.enZona = false;
        UIGameManager.instance.DesactivarMensajeAccion();
    }
}

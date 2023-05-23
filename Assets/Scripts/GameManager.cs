using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance = null;
    [SerializeField] float tiempoNivel;

    //UI variables
    [SerializeField] GameObject panelFin;
    [SerializeField] Image imagenComida;
    private TextMeshProUGUI contadorTiempo;
    private Slider tiempoSlider;
    private int minutos;
    private int segundos;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
            Destroy(this.gameObject);

        Instance = this;

        contadorTiempo = GameObject.Find("Contador tiempo").GetComponent<TextMeshProUGUI>();
        /*tiempoSlider = GameObject.Find("Tiempo slider").GetComponent<Slider>();
        tiempoSlider.maxValue = tiempoNivel;
        tiempoSlider.value = tiempoNivel;*/
    }

    // Update is called once per frame
    void Update()
    {
        ActualizarUI();

        if (tiempoNivel < 0)
        {
            Debug.Log("Se acabo el tiempo");
            panelFin.SetActive(true);
            return;
        }
        tiempoNivel -= Time.deltaTime;
    }

    private void ActualizarUI()
    {
        //tiempoSlider.value = tiempoNivel;

        minutos = (int)(tiempoNivel / 60);
        segundos = (int)(tiempoNivel - minutos * 60);
        contadorTiempo.text = String.Format("{0:00}:{1:00}", minutos, segundos);
    }

    public void SetImagenComida(Sprite spriteComida)
    {
        imagenComida.color = new Color(255, 255, 255, 1);
        imagenComida.sprite = spriteComida;
    }
}

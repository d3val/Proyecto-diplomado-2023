using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance = null;
    [SerializeField] float tiempoNivel;

    //UI variables
    [SerializeField] GameObject panelJuego;
    [SerializeField] GameObject panelPausa;
    [SerializeField] GameObject panelFin;
    [SerializeField] Image imagenComida;
    private TextMeshProUGUI contadorTiempo;
    private Slider tiempoSlider;
    private int minutos;
    private int segundos;

    public static bool juegoPausado;

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
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
                ReanudarJuego();
            else
                PausarJuego();
        }
        if (juegoPausado)
            return;

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

    public void PausarJuego()
    {
        Time.timeScale = 0;
        juegoPausado = true;
        panelJuego.SetActive(false);
        panelPausa.SetActive(true);

    }

    public void ReanudarJuego()
    {
        Time.timeScale = 1;
        juegoPausado = false;
        panelJuego.SetActive(true);
        panelPausa.SetActive(false);
    }


}

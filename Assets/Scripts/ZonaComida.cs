using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ZonaComida : MonoBehaviour
{
    [Header("Ajustes de comida generada")]
    [SerializeField] private float estaminaComida = 50f;
    [SerializeField] private float tiempoParaConsumir = 10f;
    [SerializeField] private float tiempoPreparacion = 10f;
    [SerializeField] private Sprite spriteComida = null;
    public Comida comidaServida { private set; get; }

    [Header("UI")]
    [SerializeField] GameObject slider;
    [SerializeField] Image sliderFill;
    [SerializeField] GameObject textoAccion;
    private TextMeshProUGUI texto;
    private Slider sliderPreparacion;


    // 0 = inactivo
    // 1 = en espera de orden
    // 2 = preparando orden
    // 3 = esperando entregar orden
    // 4 = Recuperando zona

    public int estado = 0;
    private bool jugadorCerca;

    //Varibales de audio
    [Header("Ajustes del audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clipCocinando;
    [SerializeField] AudioClip clipComidaLista;

    // Start is called before the first frame update
    private void Start()
    {
        sliderPreparacion = slider.GetComponent<Slider>();
        sliderPreparacion.maxValue = tiempoPreparacion;
        comidaServida = new Comida(estaminaComida, tiempoParaConsumir, spriteComida);
        texto = textoAccion.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (GameManager.juegoPausado)
            return;
        RevisarEstado();
        ActualizarUI();
    }
    private void ActualizarUI()
    {
        if (sliderPreparacion.value < sliderPreparacion.maxValue / 3)
            sliderFill.color = Color.red;
        else if (sliderPreparacion.value < sliderPreparacion.maxValue * 2 / 3)
            sliderFill.color = Color.yellow;
        else
            sliderFill.color = Color.green;
    }
    private void RevisarEstado()
    {
        switch (estado)
        {
            // Inactivo
            case 0:
                slider.SetActive(false);
                if (!UIGameManager.instance.enZona)
                    UIGameManager.instance.DesactivarMensajeAccion();
                break;
            // Esperando por orden
            case 1:
                if (UIGameManager.instance.enZona)
                    UIGameManager.instance.SetMensajeAccion("Ordenar");
                break;
            //Preparando comida
            case 2:
                slider.SetActive(true);
                sliderPreparacion.value += Time.deltaTime;
                textoAccion.SetActive(true);
                texto.text = "Preparando";
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = clipCocinando;
                    audioSource.Play();
                }
                if (sliderPreparacion.value >= sliderPreparacion.maxValue)
                {
                    audioSource.Stop();
                    audioSource.PlayOneShot(clipComidaLista);
                    estado = 3;
                }
                break;
            //Esperando por que el jugador recoja comida
            case 3:
                texto.text = "Listo";
                if (jugadorCerca)
                    UIGameManager.instance.SetMensajeAccion("Recoger");
                break;
            // Tiempo de espera hasta poder pedir otra vez
            case 4:
                sliderPreparacion.value = 0;
                slider.SetActive(false);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        jugadorCerca = true;
        UIGameManager.instance.enZona = true;

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

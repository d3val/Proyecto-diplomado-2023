using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private Slider sliderPreparacion;

    // 0 = inactivo
    // 1 = en espera de orden
    // 2 = preparando orden
    // 3 = esperando entregar orden
    // 4 = Recuperando zona
    [HideInInspector]
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
    }

    private void Update()
    {
        RevisarEstado();
    }

    private void RevisarEstado()
    {
        switch (estado)
        {
            // Inactivo
            case 0:
                slider.SetActive(false);
                UIGameManager.instance.DesactivarMensajeAccion();
                break;
            // Esperando por orden
            case 1:
                UIGameManager.instance.SetMensajeAccion("Ordenar");
                break;
            //Preparando comida
            case 2:
                slider.SetActive(true);
                sliderPreparacion.value += Time.deltaTime;
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
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        jugadorCerca = false;
    }
}

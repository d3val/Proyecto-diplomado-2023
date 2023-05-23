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
    private Comida comidaServida;

    [Header("UI")]
    [SerializeField] GameObject slider;
    private Slider sliderPreparacion;

    // Variables de trabajo
    private Jugador jugador;

    // 0 = inactivo
    // 1 = en espera de orden
    // 2 = preparando orden
    // 3 = esperando entregar orden
    private int estado = 0;
    private bool jugadorCerca;

    // Start is called before the first frame update
    private void Start()
    {
        sliderPreparacion = slider.GetComponent<Slider>();
        sliderPreparacion.maxValue = tiempoPreparacion;
        comidaServida = new Comida(estaminaComida, tiempoParaConsumir, spriteComida);
    }

    private void Update()
    {
        switch (estado)
        {
            case 1:
                UIGameManager.instance.SetMensajeAccion("Ordenar");
                if (Input.GetKeyDown(KeyCode.E))
                    estado = 2;
                break;

            case 2:
                sliderPreparacion.value += Time.deltaTime;
                if (sliderPreparacion.value >= sliderPreparacion.maxValue)
                    estado = 3;
                break;
            case 3:
                if (!jugadorCerca)
                    return;

                UIGameManager.instance.SetMensajeAccion("Recoger");

                if (Input.GetKeyDown(KeyCode.E) && jugadorCerca)
                {
                    GameManager.Instance.SetImagenComida(comidaServida.sprite);
                    sliderPreparacion.value = 0;
                    slider.SetActive(false);
                    estado = 0;
                    UIGameManager.instance.DesactivarMensajeAccion();
                    jugador.comidaActual = comidaServida;
                }
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        jugador = other.GetComponent<Jugador>();

        jugadorCerca = true;
        if (estado != 0)
            return;

        estado = 1;
        slider.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;
        jugador = null;
        UIGameManager.instance.DesactivarMensajeAccion();
        jugadorCerca = false;

        if (estado != 1)
            return;

        estado = 0;
        slider.SetActive(false);

    }
}

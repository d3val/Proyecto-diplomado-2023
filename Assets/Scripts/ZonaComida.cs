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
    float preparacion = 0f;
    [SerializeField] private Sprite spriteComida = null;
    public Comida comidaServida { private set; get; }

    private UIZona UIZona;



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
        preparacion = 0;
        UIZona = GetComponent<UIZona>();
        comidaServida = new Comida(estaminaComida, tiempoParaConsumir, spriteComida);
        UIZona.SetSliderMaxValue(tiempoPreparacion);
    }

    private void Update()
    {
        if (LevelManager.juegoPausado)
            return;
        RevisarEstado();
    }

    private void RevisarEstado()
    {
        switch (estado)
        {
            // Inactivo
            case 0:
                UIZona.DesactivarUI();
                break;
            // Esperando por orden
            case 1:
                if (LevelManager.jugadorEnZona)
                    UILevelManager.instance.SetMensajeAccion("Ordenar");
                break;
            //Preparando comida
            case 2:
                preparacion += Time.deltaTime;
                UIZona.ActivarUI();
                UIZona.ActualizarLabel("Preparando");
                UIZona.ActualizarSlider(preparacion);
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = clipCocinando;
                    audioSource.Play();
                }
                if (preparacion > tiempoPreparacion)
                {
                    audioSource.Stop();
                    audioSource.PlayOneShot(clipComidaLista);
                    estado = 3;
                }
                break;
            //Esperando por que el jugador recoja comida
            case 3:
                UIZona.ActualizarLabel("Listo");
                if (jugadorCerca)
                    UILevelManager.instance.SetMensajeAccion("Recoger");
                break;
            // Tiempo de espera hasta poder pedir otra vez
            case 4:
                preparacion = 0;
                UIZona.DesactivarUI();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        jugadorCerca = true;
        LevelManager.jugadorEnZona = true;

    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        jugadorCerca = false;
        LevelManager.jugadorEnZona = false;
        UILevelManager.instance.SetActiveMensajeAccion(false);
    }
}

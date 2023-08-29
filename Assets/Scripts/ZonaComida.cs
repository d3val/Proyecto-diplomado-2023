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
    float preparacion;
    [SerializeField] private Sprite spriteComida = null;
    public Comida comidaServida { private set; get; }

    private UIZona UIZona;

    public int estado { private set; get; }
    const int ESPERA = 1;
    const int PREPARANDO = 2;
    const int LISTO = 3;
    const int LIMPIANDO = 4;

    //Varibales de audio
    [Header("Ajustes del audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clipCocinando;
    [SerializeField] AudioClip clipComidaLista;

    // Start is called before the first frame update
    private void Start()
    {
        preparacion = 0;
        estado = ESPERA;
        UIZona = GetComponent<UIZona>();
        comidaServida = new Comida(estaminaComida, tiempoParaConsumir, spriteComida);
        UIZona.SetSliderMaxValue(tiempoPreparacion);
    }

    public IEnumerator Preparar()
    {
        estado = PREPARANDO;
        UIZona.ActivarUI();
        UIZona.ActualizarLabel("Preparando");
        if (!audioSource.isPlaying)
        {
            audioSource.clip = clipCocinando;
            audioSource.Play();
        }
        while (preparacion < tiempoPreparacion)
        {
            preparacion += Time.deltaTime;
            UIZona.ActualizarSlider(preparacion);
            yield return null;
        }

        audioSource.Stop();
        audioSource.PlayOneShot(clipComidaLista);
        estado = LISTO;
        UIZona.ActualizarLabel("Listo");
    }

    public IEnumerator Limpiar()
    {
        estado = LIMPIANDO;
        UIZona.ActualizarLabel("Limpiando");
        preparacion = 0;
        while (preparacion < tiempoPreparacion)
        {
            preparacion += Time.deltaTime;
            UIZona.ActualizarSlider(preparacion);
            yield return null;
        }
        preparacion = 0;
        estado = ESPERA;
        UIZona.DesactivarUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;


        if (estado == ESPERA)
            UILevelManager.instance.SetMensajeAccion("Ordenar");
        if (estado == LISTO)
            UILevelManager.instance.SetMensajeAccion("Recoger");

    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        UILevelManager.instance.SetActiveMensajeAccion(false);
    }

}

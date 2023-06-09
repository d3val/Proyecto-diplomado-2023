using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOpciones : MonoBehaviour
{
    [SerializeField] Slider sliderMusica;
    [SerializeField] Slider sliderEfectos;
    // Start is called before the first frame update
    void Start()
    {
        sliderMusica.value = GameManager.musicVolume;
        sliderEfectos.value = GameManager.sfxVolume;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

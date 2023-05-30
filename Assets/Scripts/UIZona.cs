using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIZona : MonoBehaviour
{
    [SerializeField] GameObject sliderZona;
    Slider slider;
    [SerializeField] Image sliderFill;
    [SerializeField] GameObject etiquetaZona;
    TextMeshProUGUI textoZona;
    // Start is called before the first frame update
    void Start()
    {
        slider = sliderZona.GetComponent<Slider>();
        textoZona = etiquetaZona.GetComponent<TextMeshProUGUI>();
    }

    public void ActualizarSlider(float valor)
    {
        slider.value = valor;
        if (slider.value < slider.maxValue / 3)
            sliderFill.color = Color.red;
        else if (slider.value < slider.maxValue * 2 / 3)
            sliderFill.color = Color.yellow;
        else
            sliderFill.color = Color.green;
    }

    public void ActualizarLabel(string texto)
    {
        textoZona.text = texto;
    }

    public void DesactivarUI()
    {
        sliderZona.SetActive(false);
        etiquetaZona.SetActive(false);
    }

    public void ActivarUI()
    {
        sliderZona.SetActive(true);
        etiquetaZona.SetActive(true);
    }

    public void SetSliderMaxValue(float value)
    {
        slider.maxValue = value;
    }
}

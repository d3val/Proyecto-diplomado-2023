using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(Slider))]
public class UISlider : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI etiqueta;
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        ActualizarEtiqueta();
    }

    public void ActualizarEtiqueta()
    {
        etiqueta.text = slider.value.ToString();
    }
}

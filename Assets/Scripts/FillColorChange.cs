using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillColorChange : MonoBehaviour
{
    Slider slider;
    Image fill;
    Image background;

    private void Start()
    {
        slider = GetComponent<Slider>();
        background = transform.GetComponentInChildren<Image>();
        fill = transform.GetChild(1).GetComponentInChildren<Image>();
        fill.color = Color.green;
    }

    public void ChangeColor()
    {
        if (slider.value <= slider.minValue)
        {
            fill.color = new Color(0, 0, 0, 0.5f);
            background.color = new Color(0, 0, 0, 0.5f);
            return;
        }
        if (slider.value < slider.maxValue / 3)
        {
            fill.color = Color.red;
            background.color = Color.white;
            return;
        }
        if (slider.value > slider.maxValue * 2 / 3)
        {
            fill.color = Color.green;
            background.color = Color.white;
            return;
        }
        if (slider.value < slider.maxValue * 2 / 3)
        {
            fill.color = Color.yellow;
            background.color = Color.white;
            return;
        }
    }
}

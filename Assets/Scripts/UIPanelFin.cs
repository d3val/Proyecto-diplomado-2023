using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;

public class UIPanelFin : MonoBehaviour
{
    public float increasingSpeed = 5f;
    public List<TextMeshProUGUI> scoreLabels;
    public TextMeshProUGUI totalScoreLabel;
    public List<float> condiciones;
    float[] limites;
    int labelsCompletas = 0;

    private void OnEnable()
    {
        condiciones = GameObject.FindObjectOfType<AtraccionesManager>().GetCondiciones();
        limites = new float[scoreLabels.Count];
        for (int i = 0; i < scoreLabels.Count; i++)
        {
            if (condiciones[i] < 0)
                condiciones[i] = 0;
            if (condiciones[i] > 100)
                condiciones[i] = 100;
            limites[i] = condiciones[i] * 100;
            StartCoroutine(Calcular(scoreLabels[i], limites[i]));
        }
    }

    IEnumerator Calcular(TextMeshProUGUI label, float limit)
    {
        float currentScore = int.Parse(label.text);
        while (currentScore != limit)
        {
            currentScore += increasingSpeed;
            if (currentScore > limit || Input.GetKeyDown(KeyCode.Mouse0))
            {
                currentScore = limit;
            }
            label.text = string.Format("{0:00000}", currentScore);
            yield return null;
        }
        labelsCompletas++;
        if (labelsCompletas == scoreLabels.Count)
        {
            StartCoroutine(CalcularTotal());
        }
    }

    IEnumerator CalcularTotal()
    {
        float totalScore = 0;
        increasingSpeed *= 2.5f;
        foreach (TextMeshProUGUI text in scoreLabels)
        {
            totalScore += int.Parse(text.text);
        }
        Debug.Log(totalScore);
        float currentScore = int.Parse(totalScoreLabel.text);
        while (currentScore != totalScore)
        {
            currentScore += increasingSpeed;
            if (currentScore > totalScore || Input.GetKeyDown(KeyCode.Mouse0))
            {
                labelsCompletas++;
                currentScore = totalScore;
            }
            totalScoreLabel.text = string.Format("{0:00000}", currentScore);
            yield return null;
        }
    }
}

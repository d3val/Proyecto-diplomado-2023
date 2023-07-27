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
    AudioSource audioSource;
    [SerializeField] AudioClip countingSound;
    [SerializeField] AudioClip completedSound;

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
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
        audioSource.clip = countingSound;
        audioSource.Play();
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
            audioSource.PlayOneShot(completedSound);
            StartCoroutine(CalcularTotal());
        }
    }

    IEnumerator CalcularTotal()
    {
        audioSource.pitch *= 1.25f;
        float totalScore = 0;
        increasingSpeed *= 2.5f;
        foreach (TextMeshProUGUI text in scoreLabels)
        {
            totalScore += int.Parse(text.text);
        }
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
        audioSource.Stop();
        audioSource.pitch /= 1.25f;
        audioSource.PlayOneShot(completedSound);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerPasos : MonoBehaviour
{
    public List<AudioClip> pasos = new List<AudioClip>();
    public AudioClip metalHit;
    public AudioSource audioSource;
    private int indice;
    // Start is called before the first frame update
    void Start()
    {
        indice = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayStep()
    {
        audioSource.PlayOneShot(pasos[indice]);
        indice++;
        if (indice >= pasos.Count)
            indice = 0;
    }

    public void PlayHitSound()
    {
        audioSource.PlayOneShot(metalHit);
    }
}

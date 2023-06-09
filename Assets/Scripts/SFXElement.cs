using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXElement : MonoBehaviour
{
    AudioSource audioSource;
    float volumenOriginal;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        volumenOriginal = audioSource.volume;
        SetVolume();
    }

    public void SetVolume()
    {
        audioSource.volume = volumenOriginal * GameManager.sfxVolume;
    }

}

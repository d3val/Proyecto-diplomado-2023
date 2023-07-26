using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXElement : MonoBehaviour
{
    [SerializeField] protected AudioSource audioSource;
    protected float volumenOriginal;
    // Start is called before the first frame update
    void Start()
    {
        volumenOriginal = audioSource.volume;
        SetVolume();
    }

    public void SetVolume()
    {
        Debug.Log(gameObject.name);
        audioSource.volume = volumenOriginal * GameManager.sfxVolume;
    }

}

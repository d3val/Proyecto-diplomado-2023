using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] AudioSource soundtrackAudioSource;
    float volumenOriginal;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            volumenOriginal = soundtrackAudioSource.volume;
            SetVolume();
            return;
        }
        Destroy(this.gameObject);
    }

    public void SetVolume()
    {
        soundtrackAudioSource.volume = volumenOriginal * GameManager.musicVolume;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private SoundManager instance;
    [SerializeField] AudioSource soundtrackAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            return;
        }
        Destroy(this.gameObject);
    }

    
}

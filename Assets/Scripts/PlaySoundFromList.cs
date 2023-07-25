using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundFromList : MonoBehaviour
{
    [SerializeField] List<AudioClip> list;
    [SerializeField] float volume = 1.0f;
    [SerializeField] AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.volume = volume;
    }

    public void PlayOneByIndex(int index)
    {
        if (index > list.Count)
        {
            Debug.Log("No se pude reproducir. Index fuera de rango");
            return;
        }

        audioSource.PlayOneShot(list[index], volume);
    }
}

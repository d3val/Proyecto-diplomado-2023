using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundFromList : MonoBehaviour
{
    [SerializeField] List<AudioClip> list;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayOneByIndex(int index)
    {
        if (index > list.Count)
        {
            Debug.Log("No se pude reproducir. Index fuera de rango");
            return;
        }

        audioSource.PlayOneShot(list[index]);
    }
}

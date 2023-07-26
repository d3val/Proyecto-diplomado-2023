using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundFromList : SFXElement
{
    [SerializeField] List<AudioClip> list;

    public void PlayOneByIndex(int index)
    {
        if (index > list.Count)
        {
            Debug.Log("No se pude reproducir. Index fuera de rango");
            return;
        }

        audioSource.PlayOneShot(list[index], audioSource.volume);
    }

    public void PlayByIndex(int index)
    {
        if (index > list.Count)
        {
            Debug.Log("No se pude reproducir. Index fuera de rango");
            return;
        }

        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = list[index];
        audioSource.Play();
    }

    public void StopPlaying()
    {
        audioSource.Stop();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UISound : MonoBehaviour
{
    [SerializeField] AudioClip sonidoBoton;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySonidoBoton()
    {
        audioSource.PlayOneShot(sonidoBoton);
    }
}

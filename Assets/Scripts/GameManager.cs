using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static float musicVolume = 1f;
    public static float sfxVolume = 1f;
    public static GameManager instance = null;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;
        CargarDatos();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        instance.SetSFXVolumes();
        SoundManager.instance.SetVolume();
    }

    public void SetSFXVolumes()
    {
        SFXElement[] SFXObjects = GameObject.FindObjectsOfType<SFXElement>();
        foreach (var item in SFXObjects)
        {
            item.GetComponent<SFXElement>().SetVolume();
        }
    }

    private void CargarDatos()
    {
        if (PlayerPrefs.HasKey("Volumen_Musica"))
        {
            //Debug.Log(PlayerPrefs.GetFloat("Volumen_Musica"));
            musicVolume = PlayerPrefs.GetFloat("Volumen_Musica");
        }

        if (PlayerPrefs.HasKey("Volumen_Efectos"))
        {
            //Debug.Log(PlayerPrefs.GetFloat("Volumen_Efectos"));
            sfxVolume = PlayerPrefs.GetFloat("Volumen_Efectos");
        }
    }

    public void GuardarDatos()
    {
        PlayerPrefs.SetFloat("Volumen_Musica", musicVolume);
        PlayerPrefs.SetFloat("Volumen_Efectos", sfxVolume);
    }
}

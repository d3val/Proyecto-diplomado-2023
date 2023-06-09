using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider SFXVolumeSlider;
    private void Awake()
    {
        Time.timeScale = 1.0f;
    }
    public void CargarEscena(int indice)
    {
        SceneManager.LoadScene(indice);
    }

    public void ActualizarVolumen()
    {
        GameManager.sfxVolume = SFXVolumeSlider.value;
        GameManager.musicVolume = musicVolumeSlider.value;
        GameManager.instance.GuardarDatos();
        GameManager.instance.SetSFXVolumes();
        SoundManager.instance.SetVolume();

    }

    public void CerrarJuego()
    {
        Application.Quit();
    }

    public void EmpezarJuego()
    {
        SceneManager.LoadScene(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1.0f;
    }
    public void CargarEscena(int indice)
    {
        SceneManager.LoadScene(indice);
    }

    public void CerrarJuego()
    {
        Application.Quit();
    }
}

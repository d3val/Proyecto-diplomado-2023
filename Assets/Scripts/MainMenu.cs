using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void CargarEscena(int indice)
    {
        SceneManager.LoadScene(indice);
    }

    public void CerrarJuego()
    {

    }
}

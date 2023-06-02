using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance = null;
    public float tiempoNivel;
    public int limiteAtraccionesRotas = 3;

    public static bool jugadorEnZona;
    public static bool juegoPausado;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
            Destroy(this.gameObject);

        Instance = this;
        Instance.ReanudarJuego();
        // ReanudarJuego();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
                ReanudarJuego();
            else
                PausarJuego();
        }
        if (juegoPausado)
            return;

        if (tiempoNivel < 0)
        {
            GameOver();
            return;
        }
        tiempoNivel -= Time.deltaTime;

    }

    public void PausarJuego()
    {
        Time.timeScale = 0;
        juegoPausado = true;
        UILevelManager.instance.SetActivePanelJuego(false);
        UILevelManager.instance.SetActivePanelPausa(true);
    }

    public void ReanudarJuego()
    {
        Time.timeScale = 1;
        juegoPausado = false;
        UILevelManager.instance.SetActivePanelJuego(true);
        UILevelManager.instance.SetActivePanelPausa(false);
    }

    public void GameOver()
    {
        Debug.Log("Se acabo el tiempo");
        UILevelManager.instance.SetActivePanelFinJuego(true);
        Time.timeScale = 0;
        juegoPausado = true;
    }

    public void CargarEscena(int indice)
    {
        SceneManager.LoadScene(indice);
    }

    public void RecargarEscenaActual()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

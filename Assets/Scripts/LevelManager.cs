using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance = null;
    public float tiempoNivel;
    public int limiteAtraccionesRotas = 3;

    //public static bool jugadorEnZona;
    public static bool juegoPausado;
    public Transform exitPoint;

    // Start is called before the first frame update
    private void Awake()
    {
        ReanudarJuego();
        if (Instance != null)
            Destroy(this.gameObject);

        Instance = this;
        //Instance.ReanudarJuego();
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
            GameOverBueno();
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

    public void GameOverBueno()
    {
        UILevelManager.instance.SetActivePanelFinJuego(true);
        Time.timeScale = 0;
        juegoPausado = true;
    }

    public void GameOverMalo()
    {
        UILevelManager.instance.panelFracaso.SetActive(true);
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

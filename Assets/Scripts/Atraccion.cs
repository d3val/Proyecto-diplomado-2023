using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class Atraccion : MonoBehaviour
{
    ZonaReparacion[] list;
    public bool visitorInteractable = true;
    public GameObject destinationPoint;
    [SerializeField] protected List<Transform> puntosAnclaje = new List<Transform>();
    protected int indexAnclaje = 0;
    protected Visitante visitante = null;
    float timer;
    [SerializeField] float startingWaitTime;

    public int avaiblePlaces;
    public List<Visitante> visitorsOnBoard = new List<Visitante>();
    protected bool isStarting = false;
    protected Animator animator;
    string idleClipName;
    public float condicionGeneral { private set; get; }/*{ private set; get; }*/
    public bool isWorking = true;
    public bool isRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        list = GetComponentsInChildren<ZonaReparacion>();
        indexAnclaje = 0;
        avaiblePlaces = puntosAnclaje.Count;
        if (visitorInteractable)
            InitializeAnimator();
    }
    protected virtual void InitializeAnimator()
    {
        animator = GetComponent<Animator>();
        idleClipName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
    }
    public float PromediarCondicion()
    {
        float promedio = 0;
        foreach (ZonaReparacion zona in list)
        {
            promedio += zona.condicion;
        }
        condicionGeneral = promedio / list.Length;
        return condicionGeneral;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!visitorInteractable)
            return;
        if (!other.CompareTag("Visitante"))
            return;

        visitante = other.GetComponent<Visitante>();
        if (indexAnclaje >= puntosAnclaje.Count)
            indexAnclaje = 0;
        if (visitante.currentAtraction == this)
        {
            SubirVisitante();
        }
    }

    protected virtual void SubirVisitante()
    {
        if (avaiblePlaces <= 0 || !isWorking || isRunning)
        {
            visitante.SelectDestination();
            return;
        }

        visitante.Subir(puntosAnclaje[indexAnclaje]);
        indexAnclaje++;
        visitorsOnBoard.Add(visitante);
        avaiblePlaces--;
        timer = 0;
        StartCoroutine(StartLaunching());
        /*visitante.Subir(puntosAnclaje[indexAnclaje]);
        indexAnclaje++;
        IniciarRonda();*/
    }

    protected virtual void IniciarRonda()
    {
    }
    IEnumerator StartLaunching()
    {
        if (!isStarting)
        {
            isStarting = true;
            while (timer < startingWaitTime)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            StartCoroutine(Ronda());
            timer = 0;
        }
    }



    protected virtual IEnumerator Ronda()
    {
        isRunning = true;
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(3);
        while (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != idleClipName)
        {
            yield return null;
        }

        TerminarRonda();
    }

    protected virtual void TerminarRonda()
    {
        foreach (Visitante visitor in visitorsOnBoard)
        {
            visitor.Bajar();
            avaiblePlaces++;
        }
        visitorsOnBoard.Clear();
        isStarting = false;
        isRunning = false;
    }

    public void ReabrirAtraccion()
    {
        isWorking = true;
    }
    public void CerrarAtraccion()
    {
        isWorking = false;
        StopAllCoroutines();
        TerminarRonda();
        /*BoxCollider collider = GetComponent<BoxCollider>();
        if (collider != null)
            GetComponent<BoxCollider>().enabled = false;
        this.enabled = false;*/
    }
}

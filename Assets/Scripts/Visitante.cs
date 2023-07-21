using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Visitante : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform destination;
    public Atraccion currentAtraction;
    public Atraccion previousAtraction;
    Animator animator;
    List<Atraccion> atraccionesDisponibles;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        SelectDestination();
    }

    public void SelectDestination()
    {
        atraccionesDisponibles = GameObject.Find("Level Manager").GetComponent<AtraccionesManager>().atraccionesVisitantes;
        int index = Random.Range(0, atraccionesDisponibles.Count);
        currentAtraction = atraccionesDisponibles[index];
        if (previousAtraction == currentAtraction || !currentAtraction.isWorking)
            SelectDestination();
        agent.SetDestination(currentAtraction.destinationPoint.transform.position);
    }

    public void DisableAgent()
    {
        agent.enabled = false;
    }

    public void Subir(Transform anclaje)
    {
        agent.enabled = false;
        transform.position = anclaje.transform.position;
        transform.rotation = anclaje.transform.rotation;
        transform.SetParent(anclaje);
        animator.SetBool("InAtraction", true);
    }

    public void Bajar()
    {
        transform.SetParent(null);
        agent.enabled = true;
        transform.position = currentAtraction.destinationPoint.transform.position;
        transform.rotation = currentAtraction.destinationPoint.transform.rotation;
        previousAtraction = currentAtraction;
        SelectDestination();
        animator.SetBool("InAtraction", false);
    }
}

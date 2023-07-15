using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Visitante : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform destination;
    public GameObject cubo;
    public Atraccion currentAtraction;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        SelectDestination();
    }
    private void Update()
    {

    }
    void SelectDestination()
    {
        List<Atraccion> atraccionesDisponibles = GameObject.Find("Level Manager").GetComponent<AtraccionesManager>().atraccionesVisitantes;
        int index = Random.Range(0, atraccionesDisponibles.Count);
        currentAtraction = atraccionesDisponibles[index];
        Debug.Log("Destino: " + currentAtraction.gameObject.name);
        agent.SetDestination(currentAtraction.destinationPoint.transform.position);
        cubo.transform.position = currentAtraction.transform.position;
    }

    public void DisableAgent()
    {
        agent.enabled = false;
    }

    public void Subir(Transform anclaje)
    {
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
        animator.SetBool("InAtraction", false);
    }
}

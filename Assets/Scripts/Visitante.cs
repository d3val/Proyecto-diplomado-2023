using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Visitante : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;
    private Transform destination;
    public Atraccion currentAtraction { get; private set; }
    Atraccion previousAtraction;
    int intentos = 0;
    int seguro = 0;

    private void OnEnable()
    {
        if (seguro > 0)
        {
            intentos = 0;
            previousAtraction = null;
            SelectDestination();
            agent.speed = Random.Range(1f, 1.5f);
        }
        seguro++;
    }

    public void SelectDestination()
    {
        if (intentos > 2)
        {
            currentAtraction = null;
            agent.SetDestination(LevelManager.Instance.exitPoint.position);
            return;
        }

        intentos++;
        int index = Random.Range(0, AtraccionesManager.Instance.atraccionesVisitantes.Count);
        currentAtraction = AtraccionesManager.Instance.atraccionesVisitantes[index];
        if (previousAtraction == currentAtraction || !currentAtraction.isWorking || currentAtraction.isRunning)
            SelectDestination();

        if (currentAtraction == null)
            return;
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
        if (currentAtraction == null) return;
        transform.SetPositionAndRotation(currentAtraction.destinationPoint.transform.position, currentAtraction.destinationPoint.transform.rotation);
        previousAtraction = currentAtraction;
        SelectDestination();
        animator.SetBool("InAtraction", false);
    }
}

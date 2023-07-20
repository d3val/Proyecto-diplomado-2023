using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Kilawea : MonoBehaviour
{
   /*[SerializeField] float launchWaitTime = 5.0f;
    float timer = 0;
    bool isLaunching = false;
    List<Visitante> visOnBoard = new List<Visitante>();
    [SerializeField] int places = 12;
    int avaiblePlaces;
    Animator animator;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        avaiblePlaces = places;
    }

    protected override void SubirVisitante()
    {
        if (visOnBoard.Count > avaiblePlaces)
            return;

        visitante.Subir(puntosAnclaje[indexAnclaje]);
        indexAnclaje++;
        visOnBoard.Add(visitante);
        avaiblePlaces--;
        timer = 0;
        StartCoroutine(StartLaunching());
    }

    IEnumerator StartLaunching()
    {
        if (!isLaunching)
        {
            Debug.Log("Este mensaje solo debe salir una vez");
            isLaunching = true;
            while (timer < launchWaitTime)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            Debug.Log("Fuego");
            animator.SetTrigger("Prepare");
            StartCoroutine(Ronda());
            timer = 0;
        }
    }

    IEnumerator Ronda()
    {
        yield return new WaitForSeconds(3);
        while (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Kilawea_Idle")
        {
            Debug.Log("Current clip: " + animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
            //Debug.Log("En juego");
            yield return null;
        }

        foreach (Visitante visitor in visOnBoard)
        {
            visitor.Bajar();
        }
        visOnBoard.Clear();
        isLaunching = false;
    }
   */
}

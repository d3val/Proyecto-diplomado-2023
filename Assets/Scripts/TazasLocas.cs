using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TazasLocas : Atraccion
{
    [SerializeField] Spin mainSpin;
    [SerializeField] List<Spin> secondarySpins;
    [SerializeField] float funTime = 30f;

    protected override void InitializeAnimator()
    {
        animator = null;
    }

    protected override IEnumerator Ronda()
    {
        mainSpin.enabled= true;
        foreach(Spin spin in secondarySpins)
        {
            spin.enabled = true;
        }
        yield return new WaitForSeconds(funTime);
        mainSpin.enabled = false;
        foreach (Spin spin in secondarySpins)
        {
            spin.enabled = false;
        }
        foreach (Visitante visitor in visitorsOnBoard)
        {
            visitor.Bajar();
            avaiblePlaces++;
        }
        visitorsOnBoard.Clear();
        isStarting = false;
    }
}

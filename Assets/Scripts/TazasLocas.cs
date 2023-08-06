using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TazasLocas : Atraccion
{
    [SerializeField] Spin mainSpin;
    [SerializeField] List<Spin> secondarySpins;
    [SerializeField] float funTime = 30f;
    // [SerializeField] AudioSource audioSource;
    [SerializeField] PlaySoundFromList playSoundFromList;

    protected override void InitializeAnimator()
    {
        animator = null;
    }

    protected override IEnumerator Ronda()
    {
        isRunning= true;
        mainSpin.enabled = true;
        playSoundFromList.PlayByIndex(0);
        foreach (Spin spin in secondarySpins)
        {
            spin.enabled = true;
        }
        yield return new WaitForSeconds(funTime);
        mainSpin.enabled = false;
        foreach (Spin spin in secondarySpins)
        {
            spin.enabled = false;
        }
        TerminarRonda();
    }

    protected override void TerminarRonda()
    {
        base.TerminarRonda();
        playSoundFromList.StopPlaying();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuedaFortuna : Atraccion
{
    [SerializeField] Spin spiner;
    [SerializeField] float gameTime = 20;
    [SerializeField] PlaySoundFromList playSoundFromList;

    protected override void InitializeAnimator()
    {
        animator = null;
    }
    protected override IEnumerator Ronda()
    {
        isRunning = true;
        spiner.enabled = true;
        //   audioSource.Play();
        playSoundFromList.PlayByIndex(0);
        yield return new WaitForSeconds(gameTime);
        TerminarRonda();
    }

    protected override void TerminarRonda()
    {
        base.TerminarRonda();
        spiner.enabled = false;
        //  audioSource.Stop();
        playSoundFromList.StopPlaying();
    }
}

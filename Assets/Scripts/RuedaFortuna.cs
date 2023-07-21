using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuedaFortuna : Atraccion
{
    [SerializeField] Spin spiner;
    [SerializeField] float gameTime = 20;
    protected override void InitializeAnimator()
    {
        animator = null;
    }
    protected override IEnumerator Ronda()
    {
        spiner.enabled = true;
        yield return new WaitForSeconds(gameTime);
        spiner.enabled = false;
        TerminarRonda();
    }
}

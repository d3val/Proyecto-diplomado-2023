using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TazasLocas : Atraccion
{

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void IniciarRonda()
    {
        StartCoroutine(Fun(visitante));
    }

    IEnumerator Fun(Visitante vis)
    {
        
        yield return new WaitForSeconds(funTime);
        vis.Bajar();
    }
}

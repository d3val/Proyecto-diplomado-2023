using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForward : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        transform.forward = Vector3.forward * -1;
    }

}

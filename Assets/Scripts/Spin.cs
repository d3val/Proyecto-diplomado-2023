using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] Vector2 speedRange = new(10, 15);
    float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = Time.deltaTime * Random.Range(speedRange.x, speedRange.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] Vector2 speedRange = new(10, 15);
    float rotationSpeed;
    // Start is called before the first frame update
    private void OnEnable()
    {
        rotationSpeed = Random.Range(speedRange.x, speedRange.y);
        int n = Random.Range(-10, 11);
        if (n > 0)
            rotationSpeed *= -1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}

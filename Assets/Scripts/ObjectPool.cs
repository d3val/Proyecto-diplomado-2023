using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject Object;
    [SerializeField] int totalObjects = 30;
    List<GameObject> objects = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < totalObjects; i++)
        {
            GameObject PooledObject = Instantiate(Object);
            PooledObject.SetActive(false);
            objects.Add(PooledObject);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (!objects[i].activeInHierarchy)
                return objects[i];
        }

        return null;
    }
}

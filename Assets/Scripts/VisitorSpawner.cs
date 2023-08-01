using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class VisitorSpawner : MonoBehaviour
{
    ObjectPool pool;
    [SerializeField] int initialVisitors = 10;
    [SerializeField] List<Transform> spawnPoints = new List<Transform>();

    private void Start()
    {
        GameObject aux;
        pool = GetComponent<ObjectPool>();
        for (int i = 0; i < initialVisitors; i++)
        {
            aux = pool.GetPooledObject();
            aux.transform.position = GetRandomPoint();
            aux.SetActive(true);
        }

        InvokeRepeating("SpawnVisitor", 8, 12);
    }

    void SpawnVisitor()
    {
        GameObject aux = pool.GetPooledObject();
        if (aux == null) return;
        aux.transform.position = GetRandomPoint();
        aux.SetActive(true);
    }

    Vector3 GetRandomPoint()
    {
        int randomIndex = Random.Range(0, spawnPoints.Count);
        return spawnPoints[randomIndex].position;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
    }
}

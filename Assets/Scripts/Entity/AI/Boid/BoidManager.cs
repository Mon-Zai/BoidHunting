using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    [SerializeField] private Boid boidPrefab;
    [SerializeField] private int boidCount = 50;
    [SerializeField] private Vector3 spawnBounds = new Vector3(20, 20, 20);

    private List<Boid> allBoids = new List<Boid>();

    void Start()
    {
        SpawnFlock();
    }
    private void SpawnFlock()
    {
        if (boidPrefab == null)
        {
            Debug.LogWarning("BoidManager needs both a boid prefab and boid settings assigned.", this);
            return;
        }

        for (int i = 0; i < boidCount; i++)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(-spawnBounds.x, spawnBounds.x),
                Random.Range(0, spawnBounds.y),
                Random.Range(-spawnBounds.z, spawnBounds.z)
            );

            Boid boid = Instantiate(boidPrefab, randomPos, Quaternion.identity, transform);
            boid.Init(this);
            allBoids.Add(boid);
        }
    }

    public List<Boid> GetNeighbors(Boid currentBoid, float radius)
    {
        List<Boid> neighbors = new List<Boid>();
        float radiusSqr = radius * radius;

        foreach (var other in allBoids)
        {
            if (other == currentBoid) continue;

            float distanceSqr = (currentBoid.transform.position - other.transform.position).sqrMagnitude;

            if (distanceSqr < radiusSqr)
            {
                neighbors.Add(other);
            }
        }

        return neighbors;
    }
}

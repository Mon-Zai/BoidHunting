using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    [SerializeField] private Boid boidPrefab;
    [SerializeField] private int boidCount = 50;
    [SerializeField] private Vector3 spawnBounds = new Vector3(20, 20, 20);
    [SerializeField] private float _width;
    [SerializeField] private float _height;
    public float Width => _width;
    public float Height => _height;

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
                3.6f,
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
    public void RemoveBoid(Boid boid)
    {
        if (allBoids.Contains(boid))
        {
            allBoids.Remove(boid);
            Destroy(boid.gameObject);
        }
    }
    public void CheckBounds(Boid boid)
    {
        Vector3 pos = boid.transform.position;
        if (pos.x > _width / 2) pos.x = -_width / 2;
        else if (pos.x < -_width / 2) pos.x = _width / 2;

        if (pos.z > _height / 2) pos.z = -_height / 2;
        else if (pos.z < -_height / 2) pos.z = _height / 2;

        boid.transform.position = pos;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(_width, 0.1f, _height));
    }
}

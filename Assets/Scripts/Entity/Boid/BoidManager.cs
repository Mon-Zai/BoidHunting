using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    [SerializeField] private Boid boidPrefab;
    [SerializeField] private float _width;
    [SerializeField] private float _height;
    private List<Boid> allBoids = new List<Boid>();
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
    public void AddBoid(Boid boid)
    {
        if (!allBoids.Contains(boid))
        {
            allBoids.Add(boid);
        }
    }
    public void RemoveBoid(Boid boid)
    {
        if (allBoids.Contains(boid))
        {
            allBoids.Remove(boid);
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

using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private LayerMask _boidLayer;
    private HashSet<Boid> _boidsInRange = new();
    void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _detectionRadius, _boidLayer);
        foreach (var collider in colliders)
        {
            Boid boid = collider.GetComponent<Boid>();
            if (boid != null)
            {
                boid.AddFoodTarget(this);
                _boidsInRange.Add(boid);
            }
        }

    }
    public void BeConsumed()
    {
        foreach (var boid in _boidsInRange)
        {
            boid.RemoveFoodTarget(this);
        }
        Destroy(gameObject);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
}

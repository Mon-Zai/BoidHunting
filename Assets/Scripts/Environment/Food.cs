using System;
using System.Collections.Generic;
using UnityEngine;
using Pool;

public class Food : MonoBehaviour, IResetable, IPoolable
{
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private LayerMask _boidLayer;
    private HashSet<Boid> _boidsInRange = new();
    public event Action<Food> OnConsumed;
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
        OnConsumed?.Invoke(this);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
    public void Reset()
    {
        _boidsInRange.Clear();
    }
    public virtual void OnGetFromPool() { Reset(); gameObject.SetActive(true); }
    public virtual void OnReturnToPool() { gameObject.SetActive(false); }
}

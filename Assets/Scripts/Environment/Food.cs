using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IResetable
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
    public static void TurnOnOff(Food food, bool active = true)
    {
        if (food == null) return;

        if (active) food.Reset();
        food.gameObject.SetActive(active);
    }

}

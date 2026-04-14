using System.Collections.Generic;
using UnityEngine;

public class Boid : Entity
{
    private BoidManager manager;
    public BoidSettings Settings;
    public GameObject Food;
    public Hunter HunterTarget;
    public bool NeighborsInRange => manager.GetNeighbors(this, Settings.NeighborRadius).Count > 0;
    public void Init(BoidManager boidManager)
    {
        manager = boidManager;
    }
    void FixedUpdate()
    {
        _acceleration = Vector3.ClampMagnitude(_acceleration, Settings.MaxAcceleration);
        Vector3 newVelocity = _rb.linearVelocity + _acceleration * Time.fixedDeltaTime;
        newVelocity = Vector3.ClampMagnitude(newVelocity, Settings.MaxSpeed);
        _rb.linearVelocity = newVelocity;
        _acceleration = Vector3.zero;
    }
    public List<Boid> GetNeighbors()
    {
        return manager.GetNeighbors(this, Settings.NeighborRadius);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Settings.NeighborRadius);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, Settings.SlowingRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, Settings.WanderRadius);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, Settings.SeparationRadius);
        if (Food != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, Food.transform.position);
        }
        if (HunterTarget != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, HunterTarget.transform.position);
        }
    }

}

using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    private Rigidbody rb;
    private BoidManager manager;
    private Vector3 acceleration = Vector3.zero;
    public BoidSettings Settings;
    public GameObject Food;
    public Hunter HunterTarget;
    public Vector3 Velocity => rb.linearVelocity;
    public Vector3 Position => rb.position;
    public bool NeighborsInRange => manager.GetNeighbors(this, Settings.NeighborRadius).Count > 0;
    public int BoidCount = 0;
    public void Init(BoidManager boidManager)
    {
        manager = boidManager;
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        // Clamp acceleration magnitude before integrating
        acceleration = Vector3.ClampMagnitude(acceleration, Settings.MaxAcceleration);

        // Integrate: new velocity = current velocity + acceleration * dt
        Vector3 newVelocity = rb.linearVelocity + acceleration * Time.fixedDeltaTime;

        // Keep movement flat on XZ plane
        //newVelocity.y = 0f;

        // Clamp to max speed
        newVelocity = Vector3.ClampMagnitude(newVelocity, Settings.MaxSpeed);

        rb.linearVelocity = newVelocity;

        // Reset acceleration accumulator for next frame
        acceleration = Vector3.zero;
    }
    void Update()
    {
        BoidCount = manager.GetNeighbors(this, Settings.NeighborRadius).Count;
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (flatVelocity.sqrMagnitude > 0.01f)
            transform.forward = flatVelocity.normalized;
    }
    public void ApplyForce(Vector3 force)
    {
        force = new Vector3(force.x, 0f, force.z);
        acceleration += force;
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

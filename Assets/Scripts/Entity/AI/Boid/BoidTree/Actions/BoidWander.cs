using UnityEngine;

public class BoidWander : ActionNode
{
    private Boid boid;
    private SteeringBehavior steering;
    public BoidWander(Boid boid, SteeringBehavior steering)
    {
        this.boid = boid;
        this.steering = steering;
    }
    protected override void PerformAction()
    {
        Debug.Log("Wandering");
        var wanderForce = steering.Wander();
        boid.ApplyForce(wanderForce);
    }
}
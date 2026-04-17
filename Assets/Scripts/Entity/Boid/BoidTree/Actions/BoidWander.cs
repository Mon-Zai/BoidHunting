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
        var wanderForce = steering.Wander(boid.Settings.WanderDistance,
                            boid.Settings.WanderRadius,
                            boid.Settings.WanderJitter, 
                            boid.transform.position,
                            boid.Settings.MaxSpeed);
        boid.ApplyForce(wanderForce);
    }
}
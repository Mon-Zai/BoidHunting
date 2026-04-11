using UnityEngine;

public class BoidArrive : ActionNode
{
    private Boid boid;
    private SteeringBehavior steering;
    public BoidArrive(Boid boid, SteeringBehavior steering)
    {
        this.boid = boid;
        this.steering = steering;
    }

    protected override void PerformAction()
    {
        if (boid.Food != null)
        {
            Debug.Log("Arriving at food");
            Vector3 arriveForce = steering.Arrive(boid.Food.transform.position);
            boid.ApplyForce(arriveForce);
        }
    }
}
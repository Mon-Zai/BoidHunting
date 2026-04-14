using UnityEngine;

public class BoidArriveFood : ActionNode
{
    private Boid boid;
    private SteeringBehavior steering;
    public BoidArriveFood(Boid boid, SteeringBehavior steering)
    {
        this.boid = boid;
        this.steering = steering;
    }

    protected override void PerformAction()
    {
        if (boid.Food != null)
        {
            Vector3 arriveForce = steering
                                .Arrive(
                                boid.Food.transform.position,
                                boid.Settings.MaxSpeed,
                                boid.Settings.MaxAcceleration,
                                boid.Settings.SlowingRadius
                                );
            boid.ApplyForce(arriveForce * boid.Settings.FoodAttractionWeight);
        }
    }
}
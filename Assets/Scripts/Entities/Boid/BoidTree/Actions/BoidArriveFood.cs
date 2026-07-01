using UnityEngine;

using AI.DecisionTree;
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
        if (boid.FoodTarget != null)
        {
            Vector3 arriveForce = steering
                                .Arrive(
                                boid.FoodTarget.transform.position,
                                boid.Settings.MaxSpeed,
                                boid.Settings.MaxAcceleration,
                                boid.Settings.SlowingRadius
                                );
            boid.ApplyForce(arriveForce * boid.Settings.FoodAttractionWeight);
        }
    }
}


using AI.DecisionTree;

public class BoidEvadeHunter : ActionNode
{
    private Boid boid;
    private SteeringBehavior steering;
    public BoidEvadeHunter(Boid boid, SteeringBehavior steering)
    {
        this.boid = boid;
        this.steering = steering;
    }
    protected override void PerformAction()
    {
        if (boid.HunterTarget != null)
        {
            Hunter hunter = boid.HunterTarget;
            var evadeForce = steering.Evade(boid.HunterTarget.transform.position,
                            hunter.Velocity, boid.Settings.EvadePredictionTime,
                            boid.Settings.MaxSpeed);
            boid.ApplyForce(evadeForce * boid.Settings.HunterRepulsionWeight);
        }
    }

}

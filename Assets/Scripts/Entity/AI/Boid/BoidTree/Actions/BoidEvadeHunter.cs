

using UnityEngine;

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
            Debug.Log("Evading hunter");
            if (hunter.TryGetComponent(out Rigidbody rb))
            {
                var evadeForce = steering.Evade(boid.HunterTarget.transform.position, rb.linearVelocity, boid.Settings.EvadePredictionTime);
                boid.ApplyForce(evadeForce);
            }
        }
    }

}

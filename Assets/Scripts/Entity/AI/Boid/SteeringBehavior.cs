using UnityEngine;

public class SteeringBehavior
{
    private Boid boid;
    public SteeringBehavior(Boid boid)
    {
        this.boid = boid;
    }
    public Vector3 Seek(Vector3 targetPosition)
    {
        Vector3 desiredVelocity = (targetPosition - boid.Position).normalized * boid.Settings.MaxSpeed;
        Vector3 steering = desiredVelocity - boid.Velocity;
        return steering;
    }
    public Vector3 Flee(Vector3 threatPosition)
    {
        return -Seek(threatPosition);
    }
    public Vector3 Arrive(Vector3 targetPosition)
    {
        Vector3 toTarget = targetPosition - boid.Position;
        float distance = toTarget.magnitude;

        if (distance < 0.01f) return Vector3.zero;

        float targetSpeed = boid.Settings.MaxSpeed;

        if (distance < boid.Settings.SlowingRadius)
        {
            targetSpeed = boid.Settings.MaxSpeed * (distance / boid.Settings.SlowingRadius);
        }

        Vector3 desired = toTarget.normalized * targetSpeed;
        Vector3 steering = desired - boid.Velocity;
        return Vector3.ClampMagnitude(steering, boid.Settings.MaxAcceleration);
    }
    public Vector3 Wander()
    {
        Vector3 wanderTarget = boid.transform.forward;
        wanderTarget += new Vector3(
            Random.Range(-1f, 1f) * boid.Settings.WanderJitter,
            Random.Range(-1f, 1f) * boid.Settings.WanderJitter,
            Random.Range(-1f, 1f) * boid.Settings.WanderJitter
        );

        wanderTarget.Normalize();
        wanderTarget *= boid.Settings.WanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, boid.Settings.WanderDistance);

        Vector3 forward = boid.Velocity.magnitude > 0.1f ? boid.Velocity.normalized : Vector3.forward;
        Quaternion rotation = Quaternion.LookRotation(forward);
        Vector3 targetWorld = boid.Position + rotation * targetLocal;

        return Seek(targetWorld);
    }
    public Vector3 Pursue(Vector3 targetPosition, Vector3 targetVelocity, float predictionTime)
    {
        var futurePosition = targetPosition + (predictionTime * targetVelocity);
        return Seek(futurePosition);
    }
    public Vector3 Evade(Vector3 threatPosition, Vector3 threatVelocity, float predictionTime)
    {
        var futurePosition = threatPosition + (predictionTime * threatVelocity);
        return Flee(futurePosition);
    }
}

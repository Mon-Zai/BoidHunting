using UnityEngine;

public class SteeringBehavior
{
    private Rigidbody _rb;
    private Vector3 _wanderTarget = Vector3.forward; // persiste entre frames

    public SteeringBehavior(Rigidbody rb)
    {
        _rb = rb;
    }
    public Vector3 Seek(Vector3 targetPosition, float maxSpeed)
    {
        Vector3 desiredVelocity = (targetPosition - _rb.position).normalized * maxSpeed;
        Vector3 steering = desiredVelocity - _rb.linearVelocity;
        return steering;
    }
    public Vector3 Flee(Vector3 threatPosition, float maxSpeed)
    {
        return -Seek(threatPosition, maxSpeed);
    }
    public Vector3 Arrive(Vector3 targetPosition, float maxSpeed, float maxAcceleration, float slowingRadius)
    {
        Vector3 toTarget = targetPosition - _rb.position;
        float distance = toTarget.magnitude;

        if (distance < 0.01f) return Vector3.zero;

        float targetSpeed = maxSpeed;

        if (distance < slowingRadius)
        {
            targetSpeed = maxSpeed * (distance / slowingRadius);
        }

        Vector3 desired = toTarget.normalized * targetSpeed;
        Vector3 steering = desired - _rb.linearVelocity;
        return Vector3.ClampMagnitude(steering, maxAcceleration);
    }
    public Vector3 Wander(float wanderDistance, float wanderRadius, float wanderJitter, Vector3 currentPosition, float maxSpeed)
    {
        _wanderTarget += new Vector3(
            Random.Range(-1f, 1f) * wanderJitter,
            0f,
            Random.Range(-1f, 1f) * wanderJitter
        );

        _wanderTarget = _wanderTarget.normalized * wanderRadius;

        Vector3 forward = _rb.linearVelocity.magnitude > 0.1f ? _rb.linearVelocity.normalized : _rb.transform.forward;
        Quaternion rotation = Quaternion.LookRotation(forward);
        Vector3 targetWorld = currentPosition + rotation * (_wanderTarget + new Vector3(0f, 0f, wanderDistance));

        return Seek(targetWorld, maxSpeed);
    }
    public Vector3 Pursue(Vector3 targetPosition, Vector3 targetVelocity, float predictionTime, float maxSpeed)
    {
        var futurePosition = targetPosition + (predictionTime * targetVelocity);
        return Seek(futurePosition, maxSpeed);
    }
    public Vector3 Evade(Vector3 threatPosition, Vector3 threatVelocity, float predictionTime, float maxSpeed)
    {
        var futurePosition = threatPosition + (predictionTime * threatVelocity);
        return Flee(futurePosition, maxSpeed);
    }
}

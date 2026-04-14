using UnityEngine;

public class PatrolState : HunterBaseState
{
    Transform[] patrolWaypoints;
    bool isReversing = false;
    int currentWaypointIndex = 0;
    float _distanceToWaypoint;
    SteeringBehavior steering;
    public PatrolState(Hunter hunter, Transform[] patrolWaypoints, SteeringBehavior steering, FOV fov)
    : base(hunter)
    {
        this.patrolWaypoints = patrolWaypoints;
        this.steering = steering;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        _distanceToWaypoint = float.MaxValue;
    }
    public override void Update()
    {
        base.Update();
        ConsumeStamina();

    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Patrol();
    }
    public override void OnExit()
    {
        base.OnExit();
    }
    public void Patrol()
    {
        if (patrolWaypoints.Length == 0) return;
        Vector3 targetPosition = patrolWaypoints[currentWaypointIndex].position;
        Vector3 steeringForce = steering.Arrive(targetPosition, hunter.Settings.MaxSpeed, hunter.Settings.MaxAcceleration, hunter.Settings.SlowingRadius);
        hunter.ApplyForce(steeringForce);
        _distanceToWaypoint = Vector3.Distance(hunter.transform.position, targetPosition);
        CheckWaypointDistance();
    }
    private void RandomReverse()
    {
        if (Random.value < 0.01f)
        {
            isReversing = !isReversing;
        }
    }
    private void CheckWaypointDistance()
    {
        if (_distanceToWaypoint < 1.5f)
        {
            hunter.Rigidbody.linearVelocity = Vector3.zero;
            if (!isReversing)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Length;
                if (currentWaypointIndex == 0)
                {
                    RandomReverse();
                }
            }
            else
            {
                currentWaypointIndex = (currentWaypointIndex - 1 + patrolWaypoints.Length) % patrolWaypoints.Length;
                if (currentWaypointIndex == patrolWaypoints.Length - 1)
                {
                    RandomReverse();
                }
            }
        }
    }
    private void ConsumeStamina()
    {
        if (hunter.Stamina <= 0f) return;
        hunter.Stamina -= Time.deltaTime * hunter.Settings.StaminaConsumptionRate;
        hunter.Stamina = Mathf.Clamp(hunter.Stamina, 0f, hunter.Settings.MaxStamina);
    }

}
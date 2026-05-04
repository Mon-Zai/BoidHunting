using UnityEngine;

public class PatrolState : HunterBaseState
{
    Transform[] patrolWaypoints;
    bool isReversing = false;
    int currentWaypointIndex = 0;
    float _distanceToWaypoint;
    SteeringBehavior steering;
    public PatrolState(Hunter hunter, Transform[] patrolWaypoints, SteeringBehavior steering)
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
        hunter.ConsumeStamina(hunter.Settings.StaminaConsumptionRate, Time.deltaTime);
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
        if (patrolWaypoints != hunter.PatrolPoint.PatrolWaypoints)
        {
            patrolWaypoints = hunter.PatrolPoint.PatrolWaypoints;
            currentWaypointIndex = 0;
            _distanceToWaypoint = float.MaxValue;
        }
        if (patrolWaypoints.Length == 0) return;
        Vector3 targetPosition = patrolWaypoints[currentWaypointIndex].position;
        Vector3 steeringForce = steering.Arrive(targetPosition, hunter.Settings.MaxSpeed, hunter.Settings.MaxAcceleration, hunter.Settings.SlowingRadius);
        hunter.ApplyForce(steeringForce);
        _distanceToWaypoint = Vector3.Distance(hunter.transform.position, new Vector3(targetPosition.x, hunter.transform.position.y, targetPosition.z));
        CheckWaypointDistance();
    }
    private void CheckWaypointDistance()
    {
        if (_distanceToWaypoint < 0.5f)
        {
            if (!isReversing)
            {
                if (currentWaypointIndex >= patrolWaypoints.Length - 1)
                {
                    isReversing = true;
                    currentWaypointIndex--;
                }
                else
                {
                    currentWaypointIndex++;
                }
            }
            else
            {
                if (currentWaypointIndex <= 0)
                {
                    isReversing = false;
                    currentWaypointIndex++;
                }
                else
                {
                    currentWaypointIndex--;
                }
            }
            hunter.CurrentPatrolTarget = patrolWaypoints[currentWaypointIndex];
        }
    }
}
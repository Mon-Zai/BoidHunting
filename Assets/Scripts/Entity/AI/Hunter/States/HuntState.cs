
using UnityEngine;

public class HuntState : HunterBaseState
{
    private SteeringBehavior steering;
    private FOV fov;
    private Boid _boid;
    public HuntState(Hunter hunter, SteeringBehavior steering, FOV fov) : base(hunter)
    {
        this.steering = steering;
        this.fov = fov;
    }
    public override void OnEnter()
    {
        base.OnEnter();
    }
    public override void Update()
    {
        base.Update();
        hunter.ConsumeStamina(hunter.Settings.StaminaConsumptionRate, Time.deltaTime);
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Chase();
    }
    private void Chase()
    {
        if (fov.NearestTarget != null && fov.NearestTarget.TryGetComponent(out Boid boid))
        {
            _boid = boid;
        }
        if (!fov.BoidOnSight || _boid == null) return;
        Vector3 steeringForce = Vector3.zero;
        float boidDistance = Vector3.Distance(hunter.transform.position, _boid.transform.position);
        if (boidDistance <= hunter.Settings.KillRange)
        {
            Debug.Log("Boid Caught!");
            fov.RemoveTarget(_boid.transform);
            _boid.GetCaught();
            _boid = null;
            hunter.ConsumeStamina(hunter.Settings.KillStaminaCost);
        }
        else if (boidDistance <= hunter.Settings.SlowingRadius / 2)
        {
            Debug.Log("Seeking Boid!");
            steeringForce = steering.Seek(_boid.transform.position, hunter.Settings.MaxSpeed);
        }
        else
        {
            Debug.Log("Pursuing Boid!");
            steeringForce = steering.Pursue(_boid.transform.position, _boid.Velocity, hunter.Settings.PursuePredictionTime, hunter.Settings.MaxSpeed);
        }
        hunter.ApplyForce(steeringForce);
    }
    public override void OnExit()
    {
        base.OnExit();
        _boid = null;
    }
}
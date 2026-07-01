using UnityEngine;

public class PatrolNPC : Entity
{
    private PatrolNPCController _controller;
    protected override void Awake()
    {
        base.Awake();
        _controller = GetComponent<PatrolNPCController>();
    }
    void FixedUpdate()
    {
        CalculateMovement();
    }
    void CalculateMovement()
    {
        _acceleration = Vector3.ClampMagnitude(_acceleration, _controller.Settings.MaxAcceleration);
        Vector3 newVelocity = _velocity + _acceleration * Time.deltaTime;
        newVelocity = Vector3.ClampMagnitude(newVelocity, _controller.Settings.MaxSpeed);
        newVelocity *= 1f - _controller.Settings.linearDrag;
        _velocity = newVelocity;
        transform.position += _velocity * Time.deltaTime;
        _acceleration = Vector3.zero;
    }
}


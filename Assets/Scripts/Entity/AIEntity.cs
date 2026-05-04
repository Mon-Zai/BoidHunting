using UnityEngine;

public class AIEntity<T> : Entity where T : AISettings
{
    [SerializeField] protected T _settings;
    private Rigidbody rb;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    protected virtual void Update()
    {
        Vector3 flatVelocity = new Vector3(_velocity.x, 0f, _velocity.z);
        if (flatVelocity.sqrMagnitude > 0.001f)
            transform.forward = flatVelocity.normalized;
    }
    protected virtual void FixedUpdate()
    {
        CalculateMovement();
    }
    protected virtual void CalculateMovement()
    {

        _acceleration = Vector3.ClampMagnitude(_acceleration, _settings.MaxAcceleration);
        Vector3 newVelocity = _velocity + _acceleration * Time.fixedDeltaTime;
        newVelocity = Vector3.ClampMagnitude(newVelocity, _settings.MaxSpeed);
        newVelocity *= 1f - _settings.linearDrag;
        if (rb == null)
        {
            _velocity = newVelocity;
            transform.position += _velocity * Time.fixedDeltaTime;
        }
        else
        {
            _velocity = new Vector3(newVelocity.x, rb.linearVelocity.y, newVelocity.z);
            rb.linearVelocity = _velocity;
        }
        _acceleration = Vector3.zero;
    }
}


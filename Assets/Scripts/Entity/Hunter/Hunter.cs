using UnityEngine;

public class Hunter : Entity
{
    [SerializeField] private HunterSettings _settings;
    [SerializeField] private Transform[] _patrolWaypoints;
    [SerializeField] private float _stamina;
    private FOV _fov;
    public HunterSettings Settings => _settings;
    public Transform[] PatrolWaypoints => _patrolWaypoints;
    public float Stamina => _stamina;
    public FOV FOV => _fov;
    protected override void Awake()
    {
        base.Awake();
        _stamina = _settings.MaxStamina;
        _fov = GetComponent<FOV>();
    }
    protected override void Update()
    {
        base.Update();
    }
    void FixedUpdate()
    {
        CalculateMovement();
    }
    void CalculateMovement()
    {
        _acceleration = Vector3.ClampMagnitude(_acceleration, _settings.MaxAcceleration);
        Vector3 newVelocity = _velocity + _acceleration * Time.deltaTime;
        newVelocity = Vector3.ClampMagnitude(newVelocity, _settings.MaxSpeed);
        newVelocity *= 1f - _settings.linearDrag;
        _velocity = newVelocity;
        transform.position += _velocity * Time.deltaTime;
        _acceleration = Vector3.zero;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Settings.SlowingRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Settings.KillRange);
    }
    public override void ApplyForce(Vector3 force)
    {
        base.ApplyForce(force);
        _acceleration = Vector3.ClampMagnitude(_acceleration, _settings.MaxAcceleration);
    }
    public void Stop()
    {
        _velocity = Vector3.zero;
        _acceleration = Vector3.zero;
    }
    public void ConsumeStamina(float amount = 0f, float rateMultiplier = 1f)
    {
        if (_stamina <= 0f) return;
        _stamina -= amount * rateMultiplier;
        _stamina = Mathf.Clamp(_stamina, 0f, _settings.MaxStamina);
    }
    public void RegenerateStamina(float amount = 0f, float rateMultiplier = 1f)
    {
        if (_stamina >= _settings.MaxStamina) return;
        _stamina += amount * rateMultiplier;
        _stamina = Mathf.Clamp(_stamina, 0f, _settings.MaxStamina);
    }
    public void SetPatrolWaypoints(Transform[] waypoints)
    {
        _patrolWaypoints = waypoints;
    }
    public override void Reset()
    {
        _stamina = _settings.MaxStamina;
        base.Reset();
    }
}
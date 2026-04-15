using UnityEngine;

public class Hunter : Entity
{
    [SerializeField] private HunterSettings _settings;
    public HunterSettings Settings => _settings;
    public float Stamina;
    protected override void Awake()
    {
        base.Awake();
        Stamina = _settings.MaxStamina;
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
}
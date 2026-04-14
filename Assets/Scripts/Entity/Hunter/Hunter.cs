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
    void FixedUpdate()
    {
        _acceleration = Vector3.ClampMagnitude(_acceleration, _settings.MaxAcceleration);
        Vector3 newVelocity = _rb.linearVelocity + _acceleration * Time.fixedDeltaTime;
        newVelocity = Vector3.ClampMagnitude(newVelocity, _settings.MaxSpeed);
        _rb.linearVelocity = newVelocity;
        _acceleration = Vector3.zero;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Settings.SlowingRadius);
        Gizmos.color = Color.blue;
    }
}
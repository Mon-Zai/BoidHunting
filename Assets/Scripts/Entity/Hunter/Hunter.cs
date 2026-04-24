using UnityEngine;

public class Hunter : AIEntity<HunterSettings>
{
    [SerializeField] private LayerMask _patrolLayer;
    [SerializeField] private PatrolPoint _patrolPoint;
    [SerializeField] private float _stamina;
    private FOV _fov;
    public HunterSettings Settings => _settings;
    public float Stamina => _stamina;
    public FOV FOV => _fov;
    public PatrolPoint PatrolPoint => _patrolPoint;
    void Awake()
    {
        _stamina = _settings.MaxStamina;
        _fov = GetComponent<FOV>();
    }
    protected override void Update()
    {
        base.Update();
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
    public Transform[] UpdatePatrolWaypoints()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 10f, _patrolLayer))
        {
            PatrolPoint patrolPoint = hit.collider.GetComponent<PatrolPoint>();
            if (patrolPoint != null)
            {
                _patrolPoint = patrolPoint;
            }
        }
        return _patrolPoint != null ? _patrolPoint.PatrolWaypoints : new Transform[0];
    }
    public override void Reset()
    {
        _stamina = _settings.MaxStamina;
        base.Reset();
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Settings.SlowingRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Settings.KillRange);
    }
}
using System.Collections.Generic;
using UnityEngine;

public class Boid : Entity
{
    private BoidManager manager;
    private float currentPersuedCooldown = 0f;
    [SerializeField] private BoidSettings _settings;
    public BoidSettings Settings => _settings;
    public GameObject Food;
    public Hunter HunterTarget;
    public bool NeighborsInRange => manager.GetNeighbors(this, Settings.NeighborRadius).Count > 0;
    public void Init(BoidManager boidManager)
    {
        manager = boidManager;
    }
    protected override void Update()
    {
        base.Update();
        PursuedTimer();

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
    private void PursuedTimer()
    {
        if (currentPersuedCooldown > 0f)
        {
            currentPersuedCooldown -= Time.deltaTime;
            if (currentPersuedCooldown <= 0f)
            {
                HunterTarget = null;
            }
        }
    }
    public List<Boid> GetNeighbors()
    {
        return manager.GetNeighbors(this, Settings.NeighborRadius);
    }
    public void SetHunterTarget(Hunter hunter)
    {
        HunterTarget = hunter;
        currentPersuedCooldown = Settings.persuedCooldown;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Settings.NeighborRadius);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, Settings.SlowingRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, Settings.WanderRadius);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, Settings.SeparationRadius);
        if (Food != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, Food.transform.position);
        }
        if (HunterTarget != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, HunterTarget.transform.position);
        }
    }
    public void GetCaught()
    {
        manager.RemoveBoid(this);
    }
}

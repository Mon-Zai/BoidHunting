using UnityEngine;

public class ObstacleAvoidance : MonoBehaviour
{
    private RaycastHit _obstacleHit;
    private float _avoidanceCooldown = 0;
    private const float CooldownFrames = 0.2f;
    public LayerMask obstacleLayer;
    public AISettings Settings;
    public Vector3 ObstaclePosition { get; private set; }
    public bool ObstacleInFront => _avoidanceCooldown > 0 || (ObstaclePosition != Vector3.zero);
    private Entity entity;

    void Awake()
    {
        entity = GetComponent<Entity>();
    }
    void Update()
    {
        if (_avoidanceCooldown > 0) _avoidanceCooldown -= Time.deltaTime;

        if (ObstaclePosition != Vector3.zero)
        {
            _avoidanceCooldown = CooldownFrames;
        }
    }
    void FixedUpdate()
    {
        Vector3 dirForward = transform.forward;
        Vector3 dirLeft = Quaternion.AngleAxis(-Settings.ObstacleAvoidanceAngle, transform.up) * dirForward;
        Vector3 dirRight = Quaternion.AngleAxis(Settings.ObstacleAvoidanceAngle, transform.up) * dirForward;

        float minDist = Settings.ObstacleAvoidanceDistance;
        bool hitFound = false;
        RaycastHit tempHit;

        Vector3[] dirs = { dirForward, dirLeft, dirRight };

        foreach (Vector3 dir in dirs)
        {
            if (Physics.Raycast(transform.position, dir, out tempHit, minDist, obstacleLayer))
            {
                _obstacleHit = tempHit;
                minDist = tempHit.distance;
                hitFound = true;
            }
        }

        ObstaclePosition = hitFound ? _obstacleHit.point : Vector3.zero;
        if (ObstacleInFront)
        {
            Vector3 evadeForce = ObstacleNormal(entity.Rigidbody.linearVelocity);
            entity.ApplyForce(evadeForce * Settings.ObstacleAvoidanceWeight);
        }
    }
    public Vector3 ObstacleNormal(Vector3 velocity)
    {
        Vector3 normal = _obstacleHit.normal;
        Vector3 slideDir = Vector3.ProjectOnPlane(velocity, normal).normalized;
        Vector3 desiredVelocity = (normal + slideDir).normalized * Settings.MaxSpeed;
        return desiredVelocity - velocity;
    }
    void OnDrawGizmosSelected()
    {
        Vector3 dirForward = transform.forward;
        Vector3 dirLeft = Quaternion.AngleAxis(-Settings.ObstacleAvoidanceAngle, Vector3.up) * dirForward;
        Vector3 dirRight = Quaternion.AngleAxis(Settings.ObstacleAvoidanceAngle, Vector3.up) * dirForward;
        float dist = Settings != null ? Settings.ObstacleAvoidanceDistance : 3f;

        Gizmos.color = ObstaclePosition != Vector3.zero ? Color.red : Color.white;
        Gizmos.DrawRay(transform.position, dirForward * dist);
        Gizmos.DrawRay(transform.position, dirLeft * dist);
        Gizmos.DrawRay(transform.position, dirRight * dist);
    }
}
using UnityEngine;

public class ObstacleAvoidance : MonoBehaviour
{
    public LayerMask obstacleLayer;
    public BoidSettings Settings;
    public Vector3 ObstaclePosition { get; private set; }
    private RaycastHit _obstacleHit;
    public bool ObstacleInFront => ObstaclePosition != Vector3.zero;

    void FixedUpdate()
    {
        Vector3 dirForward = transform.forward;
        Vector3 dirLeft = Quaternion.AngleAxis(-Settings.ObstacleAvoidanceAngle, Vector3.up) * dirForward;
        Vector3 dirRight = Quaternion.AngleAxis(Settings.ObstacleAvoidanceAngle, Vector3.up) * dirForward;

        if (Physics.Raycast(transform.position, dirForward, out _obstacleHit, Settings.ObstacleAvoidanceDistance, obstacleLayer)
        || Physics.Raycast(transform.position, dirLeft, out _obstacleHit, Settings.ObstacleAvoidanceDistance, obstacleLayer)
         || Physics.Raycast(transform.position, dirRight, out _obstacleHit, Settings.ObstacleAvoidanceDistance, obstacleLayer))
        {
            ObstaclePosition = _obstacleHit.point;

        }
        else
        {
            ObstaclePosition = Vector3.zero;
        }
    }
    public Vector3 ObstacleNormal(Vector3 velocity)
    {
        Vector3 normal = _obstacleHit.normal;
        normal.y = 0f;

        // Componente tangencial: desliza a lo largo de la pared
        Vector3 tangent = Vector3.Cross(normal, Vector3.up).normalized;

        // Usa el tangente que apunta más en la dirección del movimiento actual
        if (Vector3.Dot(tangent, velocity) < 0f)
            tangent = -tangent;

        Vector3 desiredVelocity = (normal + tangent).normalized * Settings.MaxSpeed;
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FOV : MonoBehaviour
{
    [SerializeField] private HunterSettings settings;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstructionMask;
    private Hunter _hunter;
    private HashSet<Transform> _visibleTargets = new HashSet<Transform>();
    public bool BoidOnSight { get; private set; }
    public Transform NearestTarget => _visibleTargets.OrderBy(t => Vector3.Distance(transform.position, t.position)).FirstOrDefault();
    void Awake()
    {
        _hunter = GetComponent<Hunter>();
        BoidOnSight = false;
    }

    void Start()
    {
        StartCoroutine(FOVRoutine());
    }

    IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            FieldOfView();
        }
    }

    void FieldOfView()
    {
        bool currentBoidOnSight = false;
        _visibleTargets.Clear();
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, settings.DetectionRange, targetMask);
        rangeChecks = rangeChecks.OrderBy(col => Vector3.Distance(transform.position, col.transform.position)).ToArray();
        foreach (Collider col in rangeChecks)
        {
            if (col.TryGetComponent(out Boid boid))
                boid.SetHunterTarget(_hunter);
        }

        foreach (Collider col in rangeChecks)
        {
            Transform target = col.transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) >= settings.FieldOfViewAngle / 2) continue;

            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)) continue;

            currentBoidOnSight = true;
            _visibleTargets.Add(target);
            break;
        }
        BoidOnSight = currentBoidOnSight;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, settings.DetectionRange);

        Vector3 fovLine1 = DirectionFromAngle(transform.eulerAngles.y, -settings.FieldOfViewAngle / 2);
        Vector3 fovLine2 = DirectionFromAngle(transform.eulerAngles.y, settings.FieldOfViewAngle / 2);

        Gizmos.color = BoidOnSight ? Color.green : Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + fovLine1 * settings.DetectionRange);
        Gizmos.DrawLine(transform.position, transform.position + fovLine2 * settings.DetectionRange);

        if (BoidOnSight && NearestTarget != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, NearestTarget.position);
        }
    }
    public void RemoveTarget(Transform target)
    {
        _visibleTargets.Remove(target);
    }
    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
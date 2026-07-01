using UnityEngine;

[CreateAssetMenu(fileName = "PatrolNPCSettings", menuName = "AI/Patrol NPC Settings")]
public class PatrolNPCSettings : AISettings
{
    [Header("Detection Settings")]
    public float detectionRadius = 10f;
    public float fieldOfViewAngle = 120f;
    public float alertRadius = 15f;
    [Header("Patrol Settings")]
    public float waitTimeAtWaypoint = 1f;
}
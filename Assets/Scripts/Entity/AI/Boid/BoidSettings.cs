using UnityEngine;

[CreateAssetMenu(fileName = "BoidSettings", menuName = "AI/Boid Settings")]
public class BoidSettings : ScriptableObject
{
    public float MaxSpeed = 5f;
    public float MaxAcceleration = 0.5f;
    [Header("Steering")]
    public float SlowingRadius = 2f;
    public float WanderRadius = 1f;
    public float WanderDistance = 2f;
    public float WanderJitter = 0.2f;
    public float PursuePredictionTime = 1f;
    public float EvadePredictionTime = 1f;
    public float ObstacleAvoidanceDistance = 3f;
    public float ObstacleAvoidanceWeight = 10f;
    [Range(0f, 180f)]
    public float ObstacleAvoidanceAngle = 45f;
    [Header("Neighborhood")]
    public float NeighborRadius = 3f;
    public float SeparationRadius = 1.2f;
    [Header("Weights")]
    public float SeparationWeight = 1.8f;
    public float AlignmentWeight = 1f;
    public float CohesionWeight = 1f;
}

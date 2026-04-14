using UnityEngine;

public abstract class AISettings : ScriptableObject
{
    [Header("General")]
    public float MaxSpeed = 5f;
    public float MaxAcceleration = 0.5f;
    public float SlowingRadius = 2f;
    [Header("Prediction")]
    public float PursuePredictionTime = 1f;
    public float EvadePredictionTime = 1f;
    [Header("Obstacle Avoidance")]
    public float ObstacleAvoidanceDistance = 3f;
    public float ObstacleAvoidanceWeight = 10f;
    [Range(0f, 180f)]
    public float ObstacleAvoidanceAngle = 45f;
}
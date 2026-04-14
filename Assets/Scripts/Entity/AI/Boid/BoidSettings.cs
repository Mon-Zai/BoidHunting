using UnityEngine;

[CreateAssetMenu(fileName = "BoidSettings", menuName = "AI/Boid Settings")]
public class BoidSettings : AISettings
{

    [Header("Steering")]
    public float WanderRadius = 1f;
    public float WanderDistance = 2f;
    public float WanderJitter = 0.2f;
    [Header("Neighborhood")]
    public float NeighborRadius = 3f;
    public float SeparationRadius = 1.2f;
    [Header("Weights")]
    public float SeparationWeight = 1.8f;
    public float AlignmentWeight = 1f;
    public float CohesionWeight = 1f;
    public float FoodAttractionWeight = 2f;
    public float HunterRepulsionWeight = 3f;
}

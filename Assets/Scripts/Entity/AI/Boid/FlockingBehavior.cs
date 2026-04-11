using System.Collections.Generic;
using UnityEngine;

public class FlockingBehavior
{
    private Boid boid;
    public FlockingBehavior(Boid boid)
    {
        this.boid = boid;
    }
    public Vector3 CalculateCohesion(List<Boid> neighbors)
    {
        Vector3 cohesion = Vector3.zero;

        foreach (var neighbor in neighbors)
        {
            if(neighbor == boid) continue;
            cohesion += neighbor.Position;
        }

        if (neighbors.Count > 0)
        {
            cohesion /= neighbors.Count;
        }
        Vector3 desiredVelocity = (cohesion - boid.Position).normalized * boid.Settings.MaxSpeed;
        cohesion = desiredVelocity - boid.Velocity;
        return Vector3.ClampMagnitude(cohesion, boid.Settings.MaxAcceleration);
    }
    public Vector3 CalculateSeparation(List<Boid> neighbors)
    {
        Vector3 separation = Vector3.zero;
        int count = 0;

        foreach (var neighbor in neighbors)
        {
            if(neighbor == boid) continue;
            Vector3 diff = boid.Position - neighbor.Position;
            float dist = diff.magnitude;

            if (dist > 0 && dist < boid.Settings.SeparationRadius)
            {
                separation += diff.normalized / dist;
                count++;
            }
        }
        if (count > 0)
        {
            separation /= count;
            separation = separation.normalized * boid.Settings.MaxSpeed - boid.Velocity;
        }
        return Vector3.ClampMagnitude(separation, boid.Settings.MaxAcceleration); ;
    }
    public Vector3 CalculateAlignment(List<Boid> neighbors)
    {
        Vector3 alignment = Vector3.zero;

        foreach (var neighbor in neighbors)
        {
            if(neighbor == boid) continue;
            alignment += neighbor.Velocity;
        }

        if (neighbors.Count > 0)
        {
            alignment /= neighbors.Count;
            alignment = alignment.normalized * boid.Settings.MaxSpeed;
            alignment -= boid.Velocity;
        }
        return Vector3.ClampMagnitude(alignment, boid.Settings.MaxAcceleration);

    }
    public void UpdateFlocking(List<Boid> neighbors)
    {
        if (neighbors.Count == 0) return;

        Vector3 separation = CalculateSeparation(neighbors);
        Vector3 alignment = CalculateAlignment(neighbors);
        Vector3 cohesion = CalculateCohesion(neighbors);

        boid.ApplyForce(separation * boid.Settings.SeparationWeight);
        boid.ApplyForce(alignment * boid.Settings.AlignmentWeight);
        boid.ApplyForce(cohesion * boid.Settings.CohesionWeight);
    }

}
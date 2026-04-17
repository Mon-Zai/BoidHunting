using UnityEngine;

public class BoidEvadeObstacle : ActionNode
{
    private Boid boid;
    private ObstacleAvoidance obstacleAvoidance;
    public BoidEvadeObstacle(Boid boid, ObstacleAvoidance obstacleAvoidance)
    {
        this.boid = boid;
        this.obstacleAvoidance = obstacleAvoidance;
    }
    protected override void PerformAction()
    {
        Vector3 evadeForce = obstacleAvoidance.ObstacleNormal(boid.Velocity);
        boid.ApplyForce(evadeForce * boid.Settings.ObstacleAvoidanceWeight);
    }
}
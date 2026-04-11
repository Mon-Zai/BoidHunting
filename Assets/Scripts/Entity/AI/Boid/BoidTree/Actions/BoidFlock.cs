using UnityEngine;

public class BoidFlock : ActionNode
{
    private Boid boid;
    private FlockingBehavior flocking;
    public BoidFlock(Boid boid, FlockingBehavior flocking)
    {
        this.boid = boid;
        this.flocking = flocking;
    }
    protected override void PerformAction()
    {
        Debug.Log("Flocking");
        var neighbors = boid.GetNeighbors();
        flocking.UpdateFlocking(neighbors);
    }
}
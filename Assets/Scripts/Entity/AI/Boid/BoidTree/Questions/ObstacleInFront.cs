using UnityEngine;

public class ObstacleInFront : QuestionNode
{
    private Boid boid;
    public ObstacleInFront(Boid boid, IPredicate predicate) : base(predicate)
    {
        this.boid = boid;
    }
    public override Node MakeDecision()
    {
        Debug.Log("Checking for obstacles in front");
        return base.MakeDecision();
    }
}
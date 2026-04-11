using UnityEngine;

public class BoidsNearby : QuestionNode
{
    public BoidsNearby(IPredicate predicate) : base(predicate)
    {
    }
    public override Node MakeDecision()
    {
        Debug.Log("Checking for nearby boids");
        return base.MakeDecision();
    }
}
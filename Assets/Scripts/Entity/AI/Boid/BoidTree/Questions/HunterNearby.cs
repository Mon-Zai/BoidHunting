using UnityEngine;

public class HunterNearby : QuestionNode
{
    public HunterNearby(IPredicate predicate) : base(predicate)
    {
    }
    public override Node MakeDecision()
    {
        Debug.Log("Checking for nearby hunters");
        return base.MakeDecision();
    }
}
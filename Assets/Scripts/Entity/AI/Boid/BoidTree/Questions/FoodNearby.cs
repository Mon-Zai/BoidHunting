using UnityEngine;

public class FoodNearby : QuestionNode
{
    public FoodNearby(IPredicate predicate) : base(predicate)
    {
    }
    public override Node MakeDecision()
    {
        Debug.Log("Checking for nearby food");
        return base.MakeDecision();
    }
}
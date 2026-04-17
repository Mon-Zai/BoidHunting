public abstract class ActionNode : Node
{
    public override Node MakeDecision()
    {
        PerformAction();
        return null;
    }
    protected abstract void PerformAction();
}
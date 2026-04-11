public abstract class QuestionNode : Node
{
    private Node _trueNode;
    private Node _falseNode;
    private IPredicate _predicate;
    public QuestionNode(IPredicate predicate)
    {
        _predicate = predicate;
    }
    public QuestionNode SetTrueNode(Node trueNode)
    {
        _trueNode = trueNode;
        return this;
    }
    public QuestionNode SetFalseNode(Node falseNode)
    {
        _falseNode = falseNode;
        return this;
    }
    public override Node MakeDecision()
    {
        return _predicate.Evaluate() ? _trueNode : _falseNode;
    }
}

public class DecisionTree
{
    private Node _rootNode;
    private Node currentNode;
    public void TraverseTree()
    {
        if (_rootNode == null) return;
        currentNode = _rootNode;
        while (currentNode != null)
        {
            currentNode = currentNode.MakeDecision();
        }
    }
    public void SetRoot(Node node)
    {
        _rootNode = node;
        currentNode = _rootNode;
    }
}

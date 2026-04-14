using UnityEngine;

public class BoidDecisionTree : MonoBehaviour
{
    private Boid boid;
    private DecisionTree _decisionBehaviorTree;
    private SteeringBehavior steeringBehavior;
    private FlockingBehavior flockingBehavior;
    private QuestionNode _QfoodNearbyNode;
    private QuestionNode _QhunterNearbyNode;
    private QuestionNode _QboidsNearbyNode;
    private ActionNode _AarriveNodeFood;
    private ActionNode _AevadeNode;
    private ActionNode _AwanderNode;
    private ActionNode _AflockNode;
    void Awake()
    {
        boid = GetComponent<Boid>();
        steeringBehavior = new SteeringBehavior(boid.Rigidbody);
        flockingBehavior = new FlockingBehavior(boid);

        _decisionBehaviorTree = new DecisionTree();

        _QfoodNearbyNode = new QuestionNode(new SimplePredicate(() => boid.Food != null));
        _QhunterNearbyNode = new QuestionNode(new SimplePredicate(() => boid.HunterTarget != null));
        _QboidsNearbyNode = new QuestionNode(new SimplePredicate(() => boid.NeighborsInRange));

        _AarriveNodeFood = new BoidArriveFood(boid, steeringBehavior);
        _AevadeNode = new BoidEvadeHunter(boid, steeringBehavior);
        _AwanderNode = new BoidWander(boid, steeringBehavior);
        _AflockNode = new BoidFlock(boid, flockingBehavior);

        _decisionBehaviorTree.SetRoot(_QfoodNearbyNode);
        _QfoodNearbyNode.SetTrueNode(_AarriveNodeFood).SetFalseNode(_QhunterNearbyNode);
        _QhunterNearbyNode.SetTrueNode(_AevadeNode).SetFalseNode(_QboidsNearbyNode);
        _QboidsNearbyNode.SetTrueNode(_AflockNode).SetFalseNode(_AwanderNode);
    }
    void FixedUpdate()
    {
        _decisionBehaviorTree.TraverseTree();
    }

}
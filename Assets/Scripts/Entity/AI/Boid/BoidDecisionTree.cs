using UnityEngine;

public class BoidDecisionTree : MonoBehaviour
{

    private DecisionTree _decisionBehaviorTree;
    private DecisionTree _flockBehaviorTree;
    private Boid boid;
    private ObstacleAvoidance obstacleAvoidance;

    private SteeringBehavior steeringBehavior;
    private FlockingBehavior flockingBehavior;

    //Decision Tree Nodes
    private QuestionNode foodNearbyNode;
    private QuestionNode hunterNearbyNode;
    private ActionNode arriveNode;
    private ActionNode evadeNode;
    private ActionNode wanderNode;

    //Flock Tree Nodes
    private QuestionNode boidsNearbyNode;
    private QuestionNode obstacleInFrontNode;
    private ActionNode flockNode;
    private ActionNode evadeObstacleNode;

    //Will add evade for obstacles

    void Awake()
    {
        boid = GetComponent<Boid>();
        obstacleAvoidance = GetComponent<ObstacleAvoidance>();

        steeringBehavior = new SteeringBehavior(boid);
        flockingBehavior = new FlockingBehavior(boid);

        _decisionBehaviorTree = new DecisionTree();
        _flockBehaviorTree = new DecisionTree();

        // Decision Tree
        foodNearbyNode = new FoodNearby(new SimplePredicate(() => boid.Food != null));
        hunterNearbyNode = new HunterNearby(new SimplePredicate(() => boid.HunterTarget != null));

        arriveNode = new BoidArrive(boid, steeringBehavior);
        evadeNode = new BoidEvadeHunter(boid, steeringBehavior);
        wanderNode = new BoidWander(boid, steeringBehavior);

        foodNearbyNode.SetTrueNode(arriveNode).SetFalseNode(hunterNearbyNode);
        hunterNearbyNode.SetTrueNode(evadeNode).SetFalseNode(wanderNode);
        _decisionBehaviorTree.SetRoot(foodNearbyNode);

        // Flock Tree
        boidsNearbyNode = new BoidsNearby(new SimplePredicate(() => boid.NeighborsInRange));
        obstacleInFrontNode = new ObstacleInFront(boid, new SimplePredicate(() => obstacleAvoidance.ObstacleInFront));
        evadeObstacleNode = new BoidEvadeObstacle(boid, obstacleAvoidance);
        flockNode = new BoidFlock(boid, flockingBehavior);

        _flockBehaviorTree.SetRoot(obstacleInFrontNode);
        obstacleInFrontNode.SetTrueNode(evadeObstacleNode).SetFalseNode(boidsNearbyNode);
        boidsNearbyNode.SetTrueNode(flockNode).SetFalseNode(wanderNode);
    }
    void FixedUpdate()
    {
        _flockBehaviorTree.TraverseTree();
        if (!obstacleAvoidance.ObstacleInFront)
        {
            _decisionBehaviorTree.TraverseTree();
        }

    }

}
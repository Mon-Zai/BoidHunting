using UnityEngine;
using AI.Pathfinding;

public class PatrolRoute : MonoBehaviour
{
    [SerializeField] private Node[] _waypoints;
    public Node[] Waypoints => _waypoints;
    public Node GetNode(int index)
    {
        if (index >= 0 && index < _waypoints.Length)
        {
            return _waypoints[index];
        }
        return null;
    }
}

using UnityEngine;

public class PatrolNPCTest : MonoBehaviour
{
    public Transform target;
    private PathFinder _pathFinder;

    void Awake()
    {
        _pathFinder = GetComponent<PathFinder>();
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Debug.Log("Moving to target: " + target.position);
            _pathFinder.MoveTo(target.position);
        }
    }
}

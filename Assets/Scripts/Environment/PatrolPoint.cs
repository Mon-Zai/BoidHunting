using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolWaypoints;
    public Transform[] PatrolWaypoints => _patrolWaypoints;
}

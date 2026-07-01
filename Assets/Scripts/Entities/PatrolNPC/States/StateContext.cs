using UnityEngine;

namespace Entities.PatrolNPCEntity.States
{
    public class StateContext : MonoBehaviour
    {
        [SerializeField] private Player player;
        public PathFinder PathFinder { get; private set; }
        public PatrolRoute PatrolRoute { get; private set; }
        public Vector3 LastKnownPlayerPosition { get; set; } = Vector3.zero;
        public bool SearchingPlayer { get; set; } = false;
        public Player Player => player;

        void Awake()
        {
            PatrolRoute = GetComponentInChildren<PatrolRoute>();
            PathFinder = GetComponent<PathFinder>();
        }
    }
}
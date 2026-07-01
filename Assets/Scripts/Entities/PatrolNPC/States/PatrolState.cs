using UnityEngine;

namespace Entities.PatrolNPCEntity.States
{
    public class PatrolState : BaseState
    {
        private int _currentWaypointIndex = 0;
        private Vector3 currentWaypoint;

        public PatrolState(PatrolNPCController controller, StateContext context) : base(controller, context)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            if (_context.PatrolRoute == null || _context.PatrolRoute.Waypoints == null || _context.PatrolRoute.Waypoints.Length == 0)
            {
                return;
            }

            currentWaypoint = _context.PatrolRoute.GetNode(_currentWaypointIndex).transform.position;
            _context.PathFinder.MoveTo(currentWaypoint);
        }

        public override void Update()
        {
            if (_context.PatrolRoute == null || _context.PatrolRoute.Waypoints == null || _context.PatrolRoute.Waypoints.Length == 0)
            {
                return;
            }

            if (_context.PathFinder.HasReachedDestination)
            {
                _currentWaypointIndex = (_currentWaypointIndex + 1) % _context.PatrolRoute.Waypoints.Length;
                currentWaypoint = _context.PatrolRoute.GetNode(_currentWaypointIndex).transform.position;
            }

            _context.PathFinder.MoveTo(currentWaypoint);
        }
    }
}
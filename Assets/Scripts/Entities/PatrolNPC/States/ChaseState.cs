using UnityEngine;

namespace Entities.PatrolNPCEntity.States
{
    public class ChaseState : BaseState
    {
        public ChaseState(PatrolNPCController controller, StateContext context) : base(controller, context)
        {
        }
        public override void OnEnter()
        {
            base.OnEnter();
            Collider[] colliders = Physics.OverlapSphere(_context.Player.transform.position, _controller.Settings.alertRadius);
            foreach (var collider in colliders)
            {
                if(collider.TryGetComponent<StateContext>(out var stateContext))
                {
                    stateContext.LastKnownPlayerPosition = _context.Player.transform.position;
                    stateContext.SearchingPlayer = true;
                }
            }
        }
        public override void Update()
        {
            _context.PathFinder.MoveTo(_context.Player.transform.position);
        }
    }
}
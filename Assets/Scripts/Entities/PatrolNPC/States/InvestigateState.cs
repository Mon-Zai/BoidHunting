
using UnityEngine;

namespace Entities.PatrolNPCEntity.States
{
    public class InvestigateState : BaseState
    {
        public InvestigateState(PatrolNPCController controller, StateContext context) : base(controller, context)
        {
        }
        public override void OnEnter()
        {
            base.OnEnter();
        }
        public override void Update()
        {
            if(_context.PathFinder.HasReachedDestination)
            {
                _context.SearchingPlayer = false;
            }
            Vector3 investigatePosition = _context.LastKnownPlayerPosition;
            _context.PathFinder.MoveTo(investigatePosition);
        }
    }
}
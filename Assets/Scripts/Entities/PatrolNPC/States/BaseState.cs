namespace Entities.PatrolNPCEntity.States
{
    public abstract class BaseState : IState
    {
        protected PatrolNPCController _controller;
        protected StateContext _context;

        public BaseState(PatrolNPCController controller, StateContext context)
        {
            _controller = controller;
            _context = context;
        }
        public virtual void OnEnter()
        {

        }
        public virtual void Update()
        {

        }
        public virtual void FixedUpdate()
        {

        }
        public virtual void OnExit()
        {

        }


    }
}
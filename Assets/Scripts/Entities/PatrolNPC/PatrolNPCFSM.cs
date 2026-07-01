using Entities.PatrolNPCEntity.States;
public enum PatrolNPCState
{
    PATROL,
    INVESTIGATE,
    CHASE
}

public class PatrolNPCFSM
{
    private StateMachine<PatrolNPCState> _stateMachine;
    private PatrolNPCController _controller;
    private StateContext _context;


    private PatrolState _patrolState;
    private InvestigateState _investigateState;
    private ChaseState _chaseState;

    public string CurrentState => _stateMachine.CurrentKey.ToString();

    public PatrolNPCFSM(PatrolNPCController controller, StateContext context)
    {
        _stateMachine = new StateMachine<PatrolNPCState>();
        _controller = controller;
        _context = context;

        InitializeStates();
        BuildTransitions();
        _stateMachine.ChangeState(PatrolNPCState.PATROL);
    }

    private void InitializeStates()
    {
        _patrolState = new PatrolState(_controller, _context);
        _investigateState = new InvestigateState(_controller, _context);
        _chaseState = new ChaseState(_controller, _context);

        _stateMachine.AddState(PatrolNPCState.PATROL, _patrolState);
        _stateMachine.AddState(PatrolNPCState.INVESTIGATE, _investigateState);
        _stateMachine.AddState(PatrolNPCState.CHASE, _chaseState);
    }
    private void BuildTransitions()
    {

        IPredicate hasRoute = new SimplePredicate(() => _context.PatrolRoute != null);
        IPredicate searchingPlayer = new SimplePredicate(() => _context.SearchingPlayer);
        IPredicate notSearchingPlayer = new InvertPredicate(searchingPlayer);

        IPredicate isPlayerInFOV = new SimplePredicate(() => _controller.IsPlayerInFOV);
        IPredicate isPlayerNotInFOV = new InvertPredicate(isPlayerInFOV);

        IPredicate patrolPredicate = new AndPredicate(hasRoute, isPlayerNotInFOV, notSearchingPlayer);


        var toChase = new StateTransition<PatrolNPCState>(PatrolNPCState.CHASE)
        .SetPredicate(isPlayerInFOV);

        var toPatrol = new StateTransition<PatrolNPCState>(PatrolNPCState.PATROL)
        .SetPredicate(patrolPredicate);

        var toInvestigate = new StateTransition<PatrolNPCState>(PatrolNPCState.INVESTIGATE)
        .SetPredicate(searchingPlayer);

        _stateMachine.AddTransition(PatrolNPCState.PATROL, toChase, TransitionContext.Update);
        _stateMachine.AddTransition(PatrolNPCState.PATROL, toInvestigate, TransitionContext.Update);
        _stateMachine.AddTransition(PatrolNPCState.INVESTIGATE, toChase, TransitionContext.Update);
        _stateMachine.AddTransition(PatrolNPCState.INVESTIGATE, toPatrol, TransitionContext.Update);
        _stateMachine.AddTransition(PatrolNPCState.CHASE, toInvestigate, TransitionContext.Update);
        _stateMachine.AddTransition(PatrolNPCState.CHASE, toPatrol, TransitionContext.Update);
    }
    public void UpdateFSM()
    {
        _stateMachine.Update();
    }
    public void FixedUpdateFSM()
    {
        _stateMachine.FixedUpdate();
    }
}
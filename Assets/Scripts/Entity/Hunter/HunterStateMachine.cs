public enum HunterState
{
    Idle,
    Patrol,
    Hunt
}
public class HunterStateMachine
{
    private Hunter _hunter;
    private StateMachine<HunterState> _stateMachine;
    //States
    private IState _idleState;
    private IState _patrolState;
    private IState _huntState;
    private SteeringBehavior _steering;

    public HunterStateMachine(Hunter hunter)
    {
        _stateMachine = new StateMachine<HunterState>();
        _hunter = hunter;
        FOV fov = hunter.FOV;
        _steering = new SteeringBehavior(hunter);

        _idleState = new IdleState(hunter);
        _patrolState = new PatrolState(hunter, hunter.UpdatePatrolWaypoints(), _steering);
        _huntState = new HuntState(hunter, _steering, fov);

        _stateMachine.AddState(HunterState.Idle, _idleState);
        _stateMachine.AddState(HunterState.Patrol, _patrolState);
        _stateMachine.AddState(HunterState.Hunt, _huntState);

        _stateMachine.ChangeState(HunterState.Idle);
        SetTransitions();
    }
    public void FSMUpdate()
    {
        _stateMachine.Update();
    }
    public void FSMFixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }
    private void SetTransitions()
    {
        IPredicate isTired = new SimplePredicate(() => _hunter.Stamina <= 0f);
        IPredicate isRested = new SimplePredicate(() => _hunter.Stamina >= _hunter.Settings.MaxStamina);
        IPredicate hasWaypoints = new SimplePredicate(() => _hunter.UpdatePatrolWaypoints().Length > 0);
        IPredicate seesPrey = new SimplePredicate(() => _hunter.FOV.BoidOnSight);
        IPredicate lostPrey = new SimplePredicate(() => !_hunter.FOV.BoidOnSight);

        StateTransition<HunterState> idleToPatrol = new StateTransition<HunterState>(HunterState.Patrol)
        .SetPredicate(new AndPredicate(isRested, hasWaypoints));
        _stateMachine.AddTransition(HunterState.Idle, idleToPatrol, TransitionContext.Update);

        StateTransition<HunterState> patrolToHunt = new StateTransition<HunterState>(HunterState.Hunt)
        .SetPredicate(seesPrey);
        _stateMachine.AddTransition(HunterState.Patrol, patrolToHunt, TransitionContext.Update);

        StateTransition<HunterState> huntToPatrol = new StateTransition<HunterState>(HunterState.Patrol)
        .SetPredicate(lostPrey);
        _stateMachine.AddTransition(HunterState.Hunt, huntToPatrol, TransitionContext.Update);

        StateTransition<HunterState> toIdle = new StateTransition<HunterState>(HunterState.Idle)
        .SetPredicate(isTired);
        _stateMachine.AddTransition(HunterState.Patrol, toIdle, TransitionContext.Update);
        _stateMachine.AddTransition(HunterState.Hunt, toIdle, TransitionContext.Update);
    }
}
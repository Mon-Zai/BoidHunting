using Unity.VisualScripting;
using UnityEngine;
public enum HunterState
{
    Idle,
    Patrol,
    Hunt
}
public class HunterStateMachine : MonoBehaviour
{
    private Hunter hunter;
    private FOV _fov;
    private StateMachine<HunterState> _stateMachine;
    //States
    private IState _idleState;
    private IState _patrolState;
    private IState _huntState;
    [SerializeField] private Transform[] _patrolWaypoints;
    private SteeringBehavior _steering;

    void Awake()
    {
        hunter = GetComponent<Hunter>();
        _fov = GetComponent<FOV>();
        _stateMachine = new StateMachine<HunterState>();
        _steering = new SteeringBehavior(hunter);

        _idleState = new IdleState(hunter);
        _patrolState = new PatrolState(hunter, _patrolWaypoints, _steering, _fov);
        _huntState = new HuntState(hunter, _steering, _fov);

        _stateMachine.AddState(HunterState.Idle, _idleState);
        _stateMachine.AddState(HunterState.Patrol, _patrolState);
        _stateMachine.AddState(HunterState.Hunt, _huntState);

        _stateMachine.ChangeState(HunterState.Idle);
        SetTransitions();
    }
    void Update()
    {
        _stateMachine.Update();
    }
    void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }
    private void SetTransitions()
    {
        IPredicate isTired = new SimplePredicate(() => hunter.Stamina <= 0f);
        IPredicate isRested = new SimplePredicate(() => hunter.Stamina >= hunter.Settings.MaxStamina);
        IPredicate hasWaypoints = new SimplePredicate(() => _patrolWaypoints.Length > 0);
        IPredicate seesPrey = new SimplePredicate(() => _fov.BoidOnSight);
        IPredicate lostPrey = new SimplePredicate(() => !_fov.BoidOnSight);

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
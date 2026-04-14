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
    private Rigidbody _rb;
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
        _rb = GetComponent<Rigidbody>();
        _fov = GetComponent<FOV>();
        _stateMachine = new StateMachine<HunterState>();
        _steering = new SteeringBehavior(_rb);

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


        StateTransition<HunterState> idleToPatrol = new StateTransition<HunterState>(HunterState.Patrol)
        .SetPredicate(new AndPredicate(isRested, hasWaypoints));
        _stateMachine.AddTransition(HunterState.Idle, idleToPatrol, TransitionContext.Update);


        StateTransition<HunterState> patrolToIdle = new StateTransition<HunterState>(HunterState.Idle)
        .SetPredicate(isTired);
        _stateMachine.AddTransition(HunterState.Patrol, patrolToIdle, TransitionContext.Update);
    }
}
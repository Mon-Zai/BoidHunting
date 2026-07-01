using Entities.PatrolNPCEntity.States;
using UnityEngine;

public class PatrolNPCController : MonoBehaviour
{
    [SerializeField] private PatrolNPCSettings _settings;
    public string CurrentState = string.Empty;
    private FOV _fov;
    private PatrolNPC _patrolNPC;
    private PatrolNPCFSM _fsm;

    public bool IsPlayerInFOV = false;
    public PatrolNPCSettings Settings => _settings;

    private void Awake()
    {
        _fov = GetComponent<FOV>();
        _patrolNPC = GetComponent<PatrolNPC>();
    }
    void Start()
    {
        _fsm = new PatrolNPCFSM(this, GetComponent<StateContext>());
    }
    private void Update()
    {
        CurrentState = _fsm.CurrentState;
        _fsm.UpdateFSM();
    }
    private void FixedUpdate()
    {
        _fsm.FixedUpdateFSM();
        IsPlayerInFOV = _fov.IsInFieldOfView(_settings.detectionRadius, _settings.fieldOfViewAngle);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _settings.alertRadius);
    }
}
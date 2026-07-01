using UnityEngine;

public class HunterBaseState : IState
{
    protected Hunter hunter;
    public HunterBaseState(Hunter hunter)
    {
        this.hunter = hunter;
    }
    public virtual void FixedUpdate()
    {
    }

    public virtual void OnEnter()
    {
        Debug.Log("Entering " + GetType().Name);
    }

    public virtual void OnExit()
    {
        Debug.Log("Exiting " + GetType().Name);
    }

    public virtual void Update()
    {
        if (hunter.Velocity.magnitude > 0.1f)
        {
            Vector3 flatVelocity = new Vector3(hunter.Velocity.x, 0f, hunter.Velocity.z);
            if (flatVelocity.sqrMagnitude > 0.01f)
                hunter.transform.forward = flatVelocity.normalized;
        }
    }


}
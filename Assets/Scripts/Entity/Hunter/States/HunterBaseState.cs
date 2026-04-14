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
        if (hunter.Rigidbody.linearVelocity.magnitude > 0.1f)
        {
            Vector3 flatVelocity = new Vector3(hunter.Rigidbody.linearVelocity.x, 0f, hunter.Rigidbody.linearVelocity.z);
            if (flatVelocity.sqrMagnitude > 0.01f)
                hunter.Rigidbody.transform.forward = flatVelocity.normalized;
        }
    }


}
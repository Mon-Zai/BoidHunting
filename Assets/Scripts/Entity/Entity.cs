using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected float health;
    protected Rigidbody _rb;
    protected Vector3 _acceleration;
    public Rigidbody Rigidbody => _rb;


    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    protected virtual void Update()
    {
        Vector3 flatVelocity = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);
        if (flatVelocity.sqrMagnitude > 0.01f)
            transform.forward = flatVelocity.normalized;
    }

    public float Health { get { return health; } protected set { health = value; } }
    public Rigidbody Rb { get { return _rb; } protected set { _rb = value; } }

    public void ApplyForce(Vector3 force)
    {
        force = new Vector3(force.x, 0f, force.z);
        _acceleration += force;
    }

}
using UnityEngine;
using Pool;

public abstract class Entity : MonoBehaviour, IResetable, IPoolable
{
    protected Vector3 _acceleration;
    protected Vector3 _velocity = Vector3.zero;
    public Vector3 Velocity { get { return _velocity; } private set { _velocity = value; } }
    public virtual void ApplyForce(Vector3 force)
    {
        force = new Vector3(force.x, 0f, force.z);
        _acceleration += force;
    }
    public virtual void Reset()
    {
        Debug.Log("Entity Spawned, Stats got reset");
    }
    public virtual void OnGetFromPool() { Reset(); gameObject.SetActive(true); }
    public virtual void OnReturnToPool() { gameObject.SetActive(false); }
}

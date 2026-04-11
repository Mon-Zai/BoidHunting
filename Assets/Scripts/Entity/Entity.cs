using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected float health;
    protected float maxSpeed;
    protected Rigidbody rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public float MaxSpeed { get { return maxSpeed; } protected set { maxSpeed = value; } }
    public float Health { get { return health; } protected set { health = value; } }
    public Rigidbody Rb { get { return rb; } protected set { rb = value; } }

}
using UnityEngine;

public class Hunter : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 acceleration = Vector3.forward;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        transform.forward = rb.linearVelocity.normalized;
        rb.linearVelocity += acceleration * Time.fixedDeltaTime;
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, 15f);
    }
}
using UnityEngine;

public class HunterLimit : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hunter"))
        {
            Debug.Log("Hunter limit reached! Resetting position.");
            if (other.TryGetComponent(out Hunter hunter))
            {
                other.transform.position = hunter.CurrentPatrolTarget.position;
                if(other.TryGetComponent(out Rigidbody rb))
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
        }
    }
}

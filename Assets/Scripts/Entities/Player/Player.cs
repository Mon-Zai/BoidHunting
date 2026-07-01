using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSpeed = 10f;

    [Header("Obstacle Check")]
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private float checkRadius = 0.25f;
    [SerializeField] private float checkHeight = 0.9f;
    [SerializeField] private float skin = 0.05f;

    void Update()
    {
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");
        Vector3 input = new Vector3(inputX, 0f, inputZ);

        if (input.sqrMagnitude < 0.0001f) return;

        Vector3 moveDir;

        if (cameraTransform != null)
        {
            Vector3 camForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up);

            if (camForward.sqrMagnitude < 0.0001f)
            {
                camForward = Vector3.ProjectOnPlane(cameraTransform.up, Vector3.up);
            }

            camForward.Normalize();
            Vector3 camRight = Vector3.Cross(Vector3.up, camForward).normalized;

            moveDir = (camForward * input.z + camRight * input.x).normalized;
        }
        else
        {
            moveDir = input.normalized;
        }

        float step = moveSpeed * Time.deltaTime;

        if (CanMove(moveDir, step, out RaycastHit hit))
        {
            Move(moveDir, step);
            return;
        }

        Vector3 slideDir = Vector3.ProjectOnPlane(moveDir, hit.normal).normalized;
        if (slideDir.sqrMagnitude > 0.0001f && CanMove(slideDir, step, out _))
        {
            Move(slideDir, step);
        }
    }

    private void Move(Vector3 dir, float step)
    {
        transform.position += dir * step;

        Quaternion targetRot = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);
    }

    private bool CanMove(Vector3 dir, float step, out RaycastHit hit)
    {
        Vector3 origin = transform.position + Vector3.up * checkHeight;
        float distance = step + checkRadius + skin;

        return !Physics.SphereCast(origin, checkRadius, dir, out hit, distance, obstacleMask, QueryTriggerInteraction.Ignore);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 origin = transform.position + Vector3.up * checkHeight;
        Gizmos.DrawWireSphere(origin, checkRadius);
    }
#endif
}
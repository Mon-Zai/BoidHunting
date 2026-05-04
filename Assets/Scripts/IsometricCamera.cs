using UnityEngine;

public class IsometricCamera : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform _target;

    [Header("Camera Settings")]
    [SerializeField] private Vector3 _offset = new Vector3(0f, 10f, -10f);
    [SerializeField] private float _smoothSpeed = 5f;

    void LateUpdate()
    {
        if (_target == null) return;
        Vector3 targetPosition = _target.position + _offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, _smoothSpeed * Time.deltaTime);
    }
}
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    private Vector3 _lastLookDirection = Vector3.forward;
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _accelerationSmoothTime = 0.1f;
    [SerializeField] private InputReader _inputReader;
    Rigidbody _rb;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _inputReader.MoveCanceledEvent += StopMove;
    }

    void FixedUpdate()
    {
        Look();
        Move();
    }
    public void Move()
    {
        Vector3 targetVelocity = new Vector3(_inputReader.MoveInput.x, 0, _inputReader.MoveInput.y) * _maxSpeed;
        Vector3 currentHorizontal = new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.z);
        Vector3 smoothed = Vector3.Lerp(currentHorizontal, targetVelocity, _accelerationSmoothTime);
        _rb.linearVelocity = new Vector3(smoothed.x, _rb.linearVelocity.y, smoothed.z);
    }
    public void Look()
    {
        Vector3 inputDir = new Vector3(_inputReader.MoveInput.x, 0, _inputReader.MoveInput.y);
        if (inputDir != Vector3.zero)
        {
            _lastLookDirection = inputDir;
        }

        Quaternion targetRotation = Quaternion.LookRotation(_lastLookDirection);
        _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, targetRotation, Time.fixedDeltaTime * 10f));
    }
    public void StopMove()
    {
        _rb.linearVelocity = Vector3.zero;
    }
    void OnDisable()
    {
        _inputReader.MoveCanceledEvent -= StopMove;
    }
}

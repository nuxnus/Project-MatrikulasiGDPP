using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private InputManager _input;
    [SerializeField]
    private float _walkSpeed;
    [SerializeField]
    private float _rotationSmoothTime = 0.1f;
    [SerializeField]
    private float _sprintSpeed;
    [SerializeField]
    private float _walkSprintTransition;
    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private Transform _groundDetector;
    [SerializeField]
    private float _detectorRadius;
    [SerializeField]
    private LayerMask _groundLayer;
    
    
    private Rigidbody _rigidbody;
    private float _rotationSmoothVelocity;
    private float _speed;
    private bool _isGrounded;
    
    private void Start()
    {
        _input.OnMoveInput += Move;
        _input.OnSprintInput += Sprint;
    }
    private void Update()
    {
        CheckIsGrounded();
    }
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _speed = _walkSpeed;
        _input.OnJumpInput += Jump;
    }
    
    private void OnDestroy()
    {
        _input.OnMoveInput -= Move;
        _input.OnSprintInput -= Sprint;
        _input.OnJumpInput -= Jump;
    }
    
    private void Move(Vector2 axisDirection)
    {
        if (axisDirection.magnitude >= 0.1)
        {
            float rotationAngle = Mathf.Atan2(axisDirection.x, axisDirection.y) * Mathf.Rad2Deg;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref _rotationSmoothVelocity, _rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
            Vector3 movementDirection = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;
            _rigidbody.AddForce(movementDirection * Time.deltaTime * _walkSpeed);
        }
    }
    private void Sprint(bool isSprint)
    {
        if (isSprint)
        {
            if (_speed < _sprintSpeed)
            {
                _speed = _speed + _walkSprintTransition * Time.deltaTime;
            }
        }
        else
        {
            if (_speed > _walkSpeed)
            {
                _speed = _speed - _walkSprintTransition * Time.deltaTime;
            }
        }
    }
    private void Jump()
    {
        if (_isGrounded)
        {
            Vector3 jumpDirection = Vector3.up;
            _rigidbody.AddForce(jumpDirection * _jumpForce * Time.deltaTime);
            Debug.Log("Jump" + Time.deltaTime);
        }
    }
    private void CheckIsGrounded()
    {
        _isGrounded = Physics.CheckSphere(_groundDetector.position, _detectorRadius, _groundLayer);
    }
}

using System.Runtime.CompilerServices;
using UnityEngine;

public class PlatformerMovement : MonoBehaviour
{
    [Header("Speed and acceleration")]
    [SerializeField] private float _runSpeed = 8;
    [SerializeField] private float _acceleration = 60;
    [SerializeField] private float _deceleration = 70;
    [SerializeField] private float _airAcceleration = 20;
    [SerializeField] private float _airDeceleration = 10f;

    [Header("Jump")]
    [SerializeField] private float _coyoteTime = 0.1f;
    [SerializeField] private float _jumpCutMultiplier = 0.5f;
    [SerializeField] private float _earlyJumpTime = 0.1f;
    [SerializeField] private bool _canDoubleJump = true;

    [SerializeField] private float _jumpSpeed = 15;
    [SerializeField] private float _gravity = 40;

    [Header("Ground check")]
    [SerializeField] private Transform _groundCheckTarget;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _groudnLayerMask;

    private Rigidbody2D _rididbody;

    private bool _grounded = false; 
    private bool _jumpPressedThisFrame = false;
    private bool _jumpHeld = false;
    private bool _earlyJumpTimerActive = false;

    private float _timeSinceLeftGround = 0;
    private float _timeSinceJumpPressed = float.MaxValue;

    private bool _doubleJump = false;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rididbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Debug.Log(name);
        InputManager.Instance.JumpInputPressed += OnJumpPressed;
        InputManager.Instance.JumpInputReleased += OnJumpReleased;
    }

    private void OnDisable()
    {
        InputManager.Instance.JumpInputPressed -= OnJumpPressed;
        InputManager.Instance.JumpInputReleased -= OnJumpReleased;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_grounded)
        {
            _timeSinceLeftGround += Time.deltaTime;

            if (_earlyJumpTimerActive)
            {
                _timeSinceJumpPressed += Time.deltaTime;
                if (_timeSinceJumpPressed > _earlyJumpTime)
                {
                    _earlyJumpTimerActive = false;
                }
            }
        }
        else
            _timeSinceLeftGround = 0;
    }

    private void FixedUpdate()
    {
        // Ground check
        _grounded = Physics2D.OverlapCircle(_groundCheckTarget.position, _groundCheckRadius, _groudnLayerMask);

        Vector2 velocity = _rididbody.linearVelocity;

        // Gravity
        velocity.y += _gravity * Time.fixedDeltaTime;

        // Horizontal move
        float horizontalDirection = Mathf.Clamp(InputManager.Instance.HorizontalInput, -1, 1) * _runSpeed;

        float acceleration = 0;

        // Direction
        if (Mathf.Abs(horizontalDirection) > 0.01f)
        {
            if (_grounded)
                acceleration = _acceleration;
            else
                acceleration = _airAcceleration;
        }
        else
        {
            if (_grounded)
                acceleration = _deceleration;
            else
                acceleration = _airDeceleration;
        }

        float velocityDifference = horizontalDirection - velocity.x;
        float deltaAccleration = acceleration * Time.fixedDeltaTime;
        float finallAcceleration = Mathf.Clamp(velocityDifference, -deltaAccleration, deltaAccleration);
        velocity.x += finallAcceleration;

        bool coyote = _timeSinceLeftGround <= _coyoteTime;
        bool earlyJump = _timeSinceJumpPressed <= _earlyJumpTime;

        // Jump
        if ((_grounded || coyote) && (_jumpPressedThisFrame || earlyJump))
        {
            velocity.y = _jumpSpeed;
            _grounded = false;
            _jumpPressedThisFrame = false;
            _earlyJumpTimerActive = false;
            _timeSinceJumpPressed = float.MaxValue;
        }

        // Double jump
        if (_grounded && !_doubleJump)
        {
            _doubleJump = true;
        }

        if (!_grounded && _doubleJump && _jumpPressedThisFrame)
        {
            _doubleJump = false;
            velocity.y = _jumpSpeed;
            _jumpPressedThisFrame = false;
        }

        // Variable jump height
        if (!_grounded && !_jumpHeld && velocity.y > 0f)
        {
            velocity.y *= _jumpCutMultiplier;
        }

        _rididbody.linearVelocity = velocity;

        _jumpPressedThisFrame = false;
    }

    private void OnDrawGizmos()
    {
        if (_groundCheckTarget != null)
        {
            Gizmos.color = _grounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(_groundCheckTarget.position, _groundCheckRadius);
        }
    }

    private void OnJumpPressed()
    {
        _jumpPressedThisFrame = true;
        _jumpHeld = true;

        _earlyJumpTimerActive = true;
        _timeSinceJumpPressed = 0;
    }

    private void OnJumpReleased()
    {
        _jumpHeld = false;
    }
}

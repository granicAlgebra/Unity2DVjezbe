using UnityEngine;

public class EnemySimple : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 4;
    [SerializeField] private float _acceleration = 60;
    [SerializeField] private float _deceleration = 70;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _gravity = 40;

    [Header("Ground check")]
    [SerializeField] private Transform _groundCheckTarget;
    [SerializeField] private Transform _groundCheckLeft;
    [SerializeField] private Transform _groundCheckRight;    
    [SerializeField] private Transform _wallCheckLeft;
    [SerializeField] private Transform _wallCheckRight;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _groudnLayerMask;

    [SerializeField] private LayerMask _playerLayerMask;
    [SerializeField] private float _playerSensorRadius;

    private bool _grounded = false;
    private float _direction = -1;

    private void FixedUpdate()
    {
        // Ground check
        _grounded = Physics2D.OverlapCircle(_groundCheckTarget.position, _groundCheckRadius, _groudnLayerMask);
        
        Vector2 velocity = _rigidbody.linearVelocity;
        // Gravity
        velocity.y += _gravity * Time.fixedDeltaTime;

        bool leftGrounded = Physics2D.OverlapCircle(_groundCheckLeft.position, 0.01f, _groudnLayerMask);
        bool rightGrounded = Physics2D.OverlapCircle(_groundCheckRight.position, 0.01f, _groudnLayerMask);
        bool leftWall = Physics2D.OverlapCircle(_wallCheckLeft.position, 0.01f, _groudnLayerMask);
        bool rightWall = Physics2D.OverlapCircle(_wallCheckRight.position, 0.01f, _groudnLayerMask);

        if (!leftGrounded && rightGrounded || leftWall)
            _direction = 1;
        else if (leftGrounded && !rightGrounded || rightWall)
            _direction = -1;

        bool playerSensor = Physics2D.OverlapCircle(transform.position, _playerSensorRadius, _playerLayerMask);

        float horizontalDirection = _direction * _movementSpeed;

        float acceleration = 0;

        // Direction
        if (Mathf.Abs(horizontalDirection) > 0.01f)
        {
            if (_grounded)
                acceleration = _acceleration;
        }
        else
        {
            if (_grounded)
                acceleration = _deceleration;
        }

        float velocityDifference = horizontalDirection - velocity.x;
        float deltaAccleration = acceleration * Time.fixedDeltaTime;
        float finallAcceleration = Mathf.Clamp(velocityDifference, -deltaAccleration, deltaAccleration);
        velocity.x += finallAcceleration;

        _rigidbody.linearVelocity = velocity;
    }

    private void OnDrawGizmos()
    {
        if (_groundCheckTarget != null)
        {
            Gizmos.color = _grounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(_groundCheckTarget.position, _groundCheckRadius);
            Gizmos.color = Color.yellow;    
            Gizmos.DrawWireSphere(_groundCheckLeft.position, 0.05f);
            Gizmos.DrawWireSphere(_groundCheckRight.position, 0.05f);     
            Gizmos.DrawWireSphere(_wallCheckLeft.position, 0.05f);
            Gizmos.DrawWireSphere(_wallCheckRight.position, 0.05f);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _playerSensorRadius);
        }
    }
}

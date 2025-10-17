using System.Runtime.CompilerServices;
using UnityEngine;

public class PlatformerMovement : MonoBehaviour
{
    [SerializeField] private float _runSpeed = 8;
    [SerializeField] private float _acceleration = 60;
    [SerializeField] private float _deceleration = 70;

    [SerializeField] private float _jumpSpeed = 15;
    [SerializeField] private float _gravity = 40;

    [SerializeField] private Transform _groundCheckTarget;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _groudnLayerMask;

    private Rigidbody2D _rididbody;

    private bool _grounded = false; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rididbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        // Ground check
        _grounded = Physics2D.OverlapCircle(_groundCheckTarget.position, _groundCheckRadius, _groudnLayerMask);

        Vector2 velocity = _rididbody.linearVelocity;

        // Gravity
        velocity.y += _gravity * Time.fixedDeltaTime;

        _rididbody.linearVelocity = velocity;
    }

    private void OnDrawGizmos()
    {
        if (_groundCheckTarget != null)
        {
            Gizmos.color = _grounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(_groundCheckTarget.position, _groundCheckRadius);
        }
    }
}

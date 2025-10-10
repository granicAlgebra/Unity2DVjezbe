using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class MovementExamples : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private float _verticalVelocity = 0;
    [SerializeField] private float _jumpForce = 10;
    [SerializeField] private float _raycastDistance = 1;

    private bool _doubleJump = true;
    private float _horizontalInput;
    private float _verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        //Vector2 force = new Vector2(_speed, 0);
        //_rigidbody.AddForce(force, ForceMode2D.Impulse);
    }

    private void Update()
    {
        //if (Input.GetKey(KeyCode.W))
        //{
        //    _verticalInput = 1f;
        //}
        //else if (Input.GetKey(KeyCode.S)) 
        //{
        //    _verticalInput = -1f;
        //}
        //else
        //{
        //    _verticalInput = 0;
        //}

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _raycastDistance);

            if (hit.collider != null )
            {
                _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                Debug.Log(hit.collider.name);
            } 
            else if (_doubleJump)
            {
                Vector2 velocity = _rigidbody.velocity;
                velocity.y = 0;
                _rigidbody.velocity = velocity; 
                _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                _doubleJump = false;

            }
        }

        if (Input.GetKey(KeyCode.D))    
        { 
            _horizontalInput = 1; 
        }
        else if(Input.GetKey(KeyCode.A))
        {
            _horizontalInput = -1;
        }
        else
        {
            _horizontalInput = 0;
        }
    }

    void FixedUpdate()
    {
        //_verticalVelocity += _gravity * Time.fixedDeltaTime;
        //Vector2 position = _rigidbody.position;
        //position.x += _speed * Time.fixedDeltaTime;
        //position.y = _verticalVelocity;
        //_rigidbody.MovePosition(position);

        //Vector2 force = new Vector2(_speed, 0);
        //_rigidbody.AddForce(force, ForceMode2D.Force);

        _rigidbody.AddForce(new Vector2(_speed * _horizontalInput, 0));


    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _doubleJump = true;
    }
}

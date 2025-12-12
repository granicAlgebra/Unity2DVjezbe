using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] PlatformerMovement _movement;

    [SerializeField] SpriteRenderer _spriteRenderer;
 
    void Start()
    {
        InputManager.Instance.JumpInputPressed += () => _animator.SetTrigger("Jump");
        InputManager.Instance.AttackInput += () => _animator.SetTrigger("Attack");
    }

    void Update()
    {
        _animator.SetBool("Grounded", _movement.IsGrounded);
        _animator.SetFloat("Speed", Mathf.Abs(_movement.Velocity.x));

        _animator.SetBool("Falling", _movement.Velocity.y < 0);

        if (_spriteRenderer.flipX)
        {
            if (InputManager.Instance.HorizontalInput > 0.0001) 
            {
                _spriteRenderer.flipX = false;
            }
        }
        else
        {
            if (InputManager.Instance.HorizontalInput < -0.0001)
            {
                _spriteRenderer.flipX = true;
            }
        }
       
    }
}

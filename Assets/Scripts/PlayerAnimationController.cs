using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] PlatformerMovement _movement;
 
    void Start()
    {
        InputManager.Instance.JumpInputPressed += () => _animator.SetTrigger("Jump");

    }

    void Update()
    {
        _animator.SetBool("Grounded", _movement.IsGrounded);
        _animator.SetFloat("Speed", _movement.Velocity.x);
    }
}

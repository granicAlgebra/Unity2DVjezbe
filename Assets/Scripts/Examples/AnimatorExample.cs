using UnityEngine;

public class AnimatorExample : MonoBehaviour
{
    [SerializeField] private Animator _animator;


    public void PlayAnimation()
    {
        _animator.SetTrigger("Trigger");
    }
}

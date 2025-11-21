using DG.Tweening;
using UnityEngine;

public class MovingPlatfrom : MonoBehaviour
{
    [SerializeField] float _transitionTime;
    [SerializeField] float _waitTime;
    [SerializeField] Transform _targetPosition;
    [SerializeField] Ease _ease;

    private Vector3 _lastPositon;
    private Vector3 _velocity;
    void Start()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(_waitTime);
        sequence.Append(transform.DOMove(_targetPosition.position, _transitionTime).SetEase(_ease));
        sequence.Append(transform.DOMove(transform.position, _transitionTime).SetEase(_ease));

        sequence.SetLoops(-1);
        sequence.Play();
        _lastPositon = transform.position;
    }

    private void FixedUpdate()
    {
        _velocity = (_lastPositon - transform.position) * Time.fixedDeltaTime;
        _lastPositon = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.parent = null;

            collision.rigidbody.linearVelocity += (Vector2)_velocity;
        }
    }
}

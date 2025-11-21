using System.Collections;
using UnityEngine;
using DG.Tweening;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _animationTime;
    [SerializeField] private float _restoreTime;
    [SerializeField] private Vector3 _position;
    private Coroutine _coroutine;
    private Vector3 _startPosition;
    void Start()
    {
        _startPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (_coroutine == null)
                _coroutine = StartCoroutine(AnimationCoroutine());
        }
    }

    private IEnumerator AnimationCoroutine()
    {
        if (_coroutine != null)
        {
            yield break;
        }
        float time = 0;
        while (time < _animationTime)
        {
            time += Time.deltaTime;

            transform.position = Vector3.Lerp(_startPosition, _startPosition + _position, _curve.Evaluate(time / _animationTime));
            yield return null;
        }

        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.mass = 100f;


        yield return new WaitForSeconds(_restoreTime);

        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        _rigidbody.DOMove(_startPosition,_animationTime);
        _rigidbody.DORotate(0, _animationTime);

        _coroutine = null;
    }
}

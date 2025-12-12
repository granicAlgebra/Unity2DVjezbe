using System;
using System.Collections;
using UnityEngine;

public class GateMechanic : MonoBehaviour
{
    [SerializeField] float _transitionTime;
    [SerializeField] Vector3 _endPosition;
    [SerializeField] AnimationCurve _curve;

    private Vector3 _startPosition;
    private Coroutine _animation;

    void Start()
    {
        _startPosition = transform.position;
    }

    private IEnumerator AnimationCoroutine(bool toOpen)
    {
        float time = 0;
        Vector3 currentPosition = transform.position;   

        while (time < _transitionTime)
        {
            time += Time.deltaTime;

            if (toOpen)
            {
                transform.position = Vector3.Lerp(currentPosition, _startPosition + _endPosition, _curve.Evaluate(time / _transitionTime));
            }
            else
            {
                transform.position = Vector3.Lerp(currentPosition, _startPosition, _curve.Evaluate(time / _transitionTime));
            }

            yield return null;

        }
        _animation = null;
    }

    internal void Open()
    {
        if (_animation != null) 
        {
            StopCoroutine(_animation);
        }

        _animation = StartCoroutine(AnimationCoroutine(true));
    }

    internal void Close()
    {
        if (_animation != null)
        {
            StopCoroutine(_animation);
        }

        _animation = StartCoroutine(AnimationCoroutine(false));
    }
}

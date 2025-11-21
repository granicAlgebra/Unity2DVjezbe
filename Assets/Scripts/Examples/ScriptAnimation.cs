using UnityEngine;
using DG.Tweening;
using System.Collections;

public class ScriptAnimation : MonoBehaviour
{
    [SerializeField] float _transitionTime;
    [SerializeField] float _rotationTime;
    [SerializeField] Vector3 _endPosition;
    [SerializeField] AnimationCurve _curve;
    [SerializeField] Ease _ease;
    [SerializeField] Transform _target;

    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }

    private void Sequence()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(transform.position + _endPosition, _transitionTime).SetEase(_ease));
        sequence.Append(transform.DOMove(transform.position, _transitionTime).SetEase(_ease));

        sequence.SetLoops(-1);
        sequence.Play();

        Sequence sequence2 = DOTween.Sequence();
        sequence2.Append(transform.DORotate(new Vector3(0, 0, 180), _rotationTime).SetEase(Ease.Linear));
        sequence2.Append(transform.DORotate(new Vector3(0, 0, 360), _rotationTime).SetEase(Ease.Linear));
        sequence2.SetLoops(-1);
        sequence2.Play();
    }

    public void AnimationTest()
    {
        transform.position = _startPosition;
        transform.DOMove(transform.position + _endPosition, _transitionTime).SetEase(_ease);
    }

    public void AnimationTest2()
    {
        StartCoroutine(AnimationCoroutine());
    }

    private IEnumerator AnimationCoroutine()
    {
        float time = 0;
        while (time < _transitionTime)
        {
            time += Time.deltaTime;

            //transform.position = Vector3.Lerp(_startPosition, _startPosition + _endPosition, CubicIn(time / _transitionTime));
            transform.position = Vector3.Lerp(_startPosition, _startPosition + _endPosition, _curve.Evaluate(time / _transitionTime));
            yield return null;
        }
    }

    private float CubicIn(float t)
    {
        return t * t;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _transitionTime * Time.deltaTime);
    }
}
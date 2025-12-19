using UnityEngine;
using DG.Tweening;
using System.Collections;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _mainClip;
    [SerializeField] private AudioClip _puzzleClip;

    private Coroutine _musicCoroutine;
    private Tween _fadeTween;

    public void ChangeToPuzzleMusic()
    {
        if (_musicCoroutine != null )
        {
            StopCoroutine(_musicCoroutine);
            _fadeTween.Kill();
        }

        _musicCoroutine = StartCoroutine(ChangeMusic(_puzzleClip));
    }

    public void ChangeToMainMusic()
    {
        if (_musicCoroutine != null)
        {
            StopCoroutine(_musicCoroutine);
            _fadeTween.Kill();
        }

        _musicCoroutine = StartCoroutine(ChangeMusic(_mainClip));
    }

    private IEnumerator ChangeMusic(AudioClip clip)
    {
        _fadeTween = _audioSource.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.5f);
        _audioSource.clip = clip;
        _audioSource.Play();
        _fadeTween = _audioSource.DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.5f);

        _musicCoroutine = null;
    }
}

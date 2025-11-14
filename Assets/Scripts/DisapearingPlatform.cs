using System.Collections;
using UnityEngine;

public class DisapearingPlatform : MonoBehaviour
{
    [SerializeField] private float _disappearTime = 0.5f;
    [SerializeField] private float _appearTime =5f;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private SpriteRenderer _renderer;

    private Coroutine _coroutine;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (_coroutine == null)
                _coroutine = StartCoroutine(DisappearingCoroutine());
        }
    }

    private IEnumerator DisappearingCoroutine()
    {
        yield return new WaitForSecondsRealtime(_disappearTime);

        _collider.enabled = false;
        _renderer.enabled = false;

        yield return new WaitForSecondsRealtime(_appearTime);
        _collider.enabled = true;
        _renderer.enabled = true;
        _coroutine = null;
    }
}

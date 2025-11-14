using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] float _time = 1.0f;

    private float _timePassed = 0;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _timePassed += Time.fixedDeltaTime;

            if (_timePassed > _time) 
            {
                GameplayManager.Instance.RemoveHeart();
                _timePassed = 0;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _timePassed = 0;
    }
}

using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _coins = 1;
    [SerializeField] private AudioClip _cointSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameplayManager.Instance.AddCoins(_coins);
            gameObject.SetActive(false);
            SfxManagar.Instance.PlaySFX(_cointSFX);
        }
    }

}

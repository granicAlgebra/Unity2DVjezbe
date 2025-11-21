using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _coins = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameplayManager.Instance.AddCoins(_coins);
            Destroy(this.gameObject);
        }
    }

}

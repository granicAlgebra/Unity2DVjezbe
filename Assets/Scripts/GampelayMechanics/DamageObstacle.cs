using UnityEngine;

public class DamageObstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameplayManager.Instance.RemoveHeart();            
        }
    }
}

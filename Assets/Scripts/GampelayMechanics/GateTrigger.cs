using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    [SerializeField] private GateMechanic _gate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _gate.Open();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _gate.Close();
        }
    }
}

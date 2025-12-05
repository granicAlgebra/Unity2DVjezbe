using UnityEngine;

public class GateKeyTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _key;
    [SerializeField] private GateMechanic _gate;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _key)
        {
            _gate.Open();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == _key)
        {
            _gate.Close();
        }
    }
}

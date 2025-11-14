using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float _upForce = 10;
    [SerializeField] private float _forceMultiplier = 1.2f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody2D playerRigidbody = collision.rigidbody;
            var velocity = playerRigidbody.linearVelocity;

            velocity.y = Mathf.Abs(collision.relativeVelocity.y) * _forceMultiplier + _upForce;
            velocity.x = collision.relativeVelocity.x * _forceMultiplier;
            playerRigidbody.linearVelocity = velocity;


            //playerRigidbody.AddForceY(_upForce, ForceMode2D.Impulse);
        }
     }
}

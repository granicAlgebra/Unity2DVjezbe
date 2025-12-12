using System.Collections.Generic;
using UnityEngine;

public class SwordWeapon : Weapon
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] private Vector2 _offsetPosition;
    [SerializeField] private Vector2 _boxSize;
    [SerializeField] private ContactFilter2D _contactFilter;

    public override void Attack()
    {
        List<Collider2D> colliders = new();
        var offset = _offsetPosition;

        if (_spriteRenderer.flipX)
        {
            offset.x *= -1;
        }
       
        Physics2D.OverlapBox((Vector2)transform.position + offset, _boxSize, 0, _contactFilter, colliders);

        for (int i = 0; i < colliders.Count; i++) 
        {
            if (colliders[i].CompareTag("Enemy"))
            {
                var enemy = colliders[i].GetComponent<EnemySimple>();
                if (enemy != null) 
                {
                    enemy.TakeDamage(Damage);
                }

                Debug.Log($"Enemy {colliders[i].name} -{Damage}HP");
            }
        }
    }

    private void OnDrawGizmos()
    {
        var offset = _offsetPosition;

        if (_spriteRenderer.flipX)
        {
            offset.x *= -1;
        }
        Gizmos.DrawWireCube(transform.position + (Vector3)offset, (Vector3) _boxSize);
    }
}

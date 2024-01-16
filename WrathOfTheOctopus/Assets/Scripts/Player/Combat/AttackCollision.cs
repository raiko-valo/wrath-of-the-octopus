using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    public int Damage = Player.Instance.Damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attack(collision);
    }

    private void Attack(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.OnRemoveHealth(Damage);
        }
    }

    public void EndAttackMove()
    {
        Destroy(gameObject);
    }
}

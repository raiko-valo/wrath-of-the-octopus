using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    public int Damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attack(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
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

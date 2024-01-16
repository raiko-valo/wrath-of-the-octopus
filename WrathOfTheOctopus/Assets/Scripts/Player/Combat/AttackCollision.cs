using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attack(collision);
    }

    private void Attack(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.OnRemoveHealth(Player.Instance.Damage);
        }
    }

    public void EndAttackMove()
    {
        Destroy(gameObject);
    }
}

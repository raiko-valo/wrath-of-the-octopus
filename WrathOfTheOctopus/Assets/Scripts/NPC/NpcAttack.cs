using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAttack : MonoBehaviour
{
    public float NextAttack;
    public float AttackCooldown;
    public int Damage;

    private Health healt;

    private void Start()
    {
        NextAttack = Time.time;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();
        print("siin");

        if (health != null && Time.time >= NextAttack)
        {
            Events.RemoveHealth(Damage);
            NextAttack += AttackCooldown;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();

        if (health != null && Time.time >= NextAttack)
        {
            Events.RemoveHealth(Damage);
            NextAttack += AttackCooldown;
        }
    }
}

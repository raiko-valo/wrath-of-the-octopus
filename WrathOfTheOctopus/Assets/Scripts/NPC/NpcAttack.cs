using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAttack : MonoBehaviour
{
    public float NextAttack;
    public float AttackCooldown;
    public int Damage;
    private Animator animator;


    private Health healt;

    private void Start()
    {
        NextAttack = Time.time;
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();

        if (health != null && Time.time >= NextAttack)
        {
            Events.RemoveHealth(Damage);
            animator.SetBool("Attack", true);
            NextAttack += AttackCooldown;
        }
        animator.SetBool("Attack", false);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();

        if (health != null && Time.time >= NextAttack)
        {
            Events.RemoveHealth(Damage);
            animator.SetBool("Attack", true);
            NextAttack += AttackCooldown;
        }
        animator.SetBool("Attack", false);
    }
}

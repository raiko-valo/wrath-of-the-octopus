using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int health = 0;
    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OnRemoveHealth(int amount)
    {
        
        health -= amount;
        animator.SetBool("Hurt", true);
        animator.SetBool("Hurt", false);
        if (health <= 0)
        {
            animator.SetInteger("Health", 0);
            Destroy(gameObject);
        }
    }
}

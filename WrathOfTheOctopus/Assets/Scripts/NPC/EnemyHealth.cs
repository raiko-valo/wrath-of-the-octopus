using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public ItemData DroppedResource = null;
    [SerializeField]
    public AudioClipGroup audioClipHurt;
    private int health = 0;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }
    public void OnRemoveHealth(int amount)
    {
        
        health -= amount;
        animator.SetTrigger("Hurt");
        audioClipHurt.Play();
        animator.SetInteger("Health", health);
        if (health <= 0)
        {
            StartCoroutine(DestroyAfterAnimation());
            if (DroppedResource != null)
            {
                DroppedResource.Drop(gameObject.transform.position);
            }
        }
    }

    IEnumerator DestroyAfterAnimation()
    {
        // Wait for the end of the "Hurt" animation
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Wait for the end of the "Death" animation
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length*2.5f);

        Destroy(gameObject);
    }
}

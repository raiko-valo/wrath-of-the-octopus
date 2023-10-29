using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public ItemData Item;
    public int ItemDropAmount;
    public int NodeHealth;
    public int ToolLevelRequired;
    public float MiningRange;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();    
    }

    [ContextMenu("DropItems")]
    public void DropItems()
    {
        for (int item = 0; item < ItemDropAmount; item++)
        {
            float angle = item * (360f / ItemDropAmount); // Calculate the angle for each object.

            // Calculate the position based on the angle and radius.
            float x = transform.position.x + 0.5f * Mathf.Cos(angle * Mathf.Deg2Rad);
            float z = transform.position.z + 0.5f * Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector3 spawnPosition = new Vector3(x, transform.position.y, z);

            Item.Drop(spawnPosition);
        }
    }

   
    private void OnMouseEnter()
    {
        if (Player.Instance.InRange(transform.position, MiningRange)) 
            spriteRenderer.color = Color.gray;
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = Color.white;
    }

    private void OnMouseDown()
    {
        if (Player.Instance.InRange(transform.position, MiningRange))
        {
            NodeHealth -= 1;
            if (NodeHealth <= 0)
            {
                DropItems();
                Destroy(gameObject);
            }
        }       
    }
}

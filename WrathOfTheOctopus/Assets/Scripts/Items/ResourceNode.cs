using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResourceNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

    private void Update()
    {
        if (Player.Instance.InRange(transform.position, MiningRange))
        {

            if (IsMouseOverObject())
            {
                spriteRenderer.color = Color.gray;
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Mine();
                }
            }
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
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

    bool IsMouseOverObject()
    {
        // Cast a ray from the mouse position into the scene
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, 100.0f,LayerMask.GetMask("ResourceNode"));

        // Check if the ray hits a collider
        if (hit.collider != null)
        {
            // Check if the collider belongs to the desired GameObject
            if (hit.collider.gameObject == gameObject)
            {
                return true;
            }
        }

        return false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        spriteRenderer.color = Color.gray;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        spriteRenderer.color = Color.white;
    }

    void Mine() 
    {
        ToolData tool = InventoryController.Instance.SelectedItem as ToolData;
        if (tool != null)
        {
            if (tool.ToolLevel >= ToolLevelRequired)
            {
                NodeHealth -= tool.Damage;
                if (NodeHealth <= 0)
                {
                    DropItems();
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            if (ToolLevelRequired == 0)
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
}

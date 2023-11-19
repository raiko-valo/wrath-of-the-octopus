using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData ItemData;
    private SpriteRenderer spriteRenderer;
    private float PickUpRange = 2f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (Player.Instance.InRange(transform.position, PickUpRange))
        {
            InventoryController.Instance.AddItem(ItemData);
            Destroy(gameObject);
        }
    }

    private void OnMouseEnter()
    {
        if (Player.Instance.InRange(transform.position, PickUpRange)) 
            spriteRenderer.color = Color.gray;
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = Color.white;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData ItemData;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }

    private void OnMouseEnter()
    {
        spriteRenderer.color = Color.gray;
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = Color.white;
    }
}

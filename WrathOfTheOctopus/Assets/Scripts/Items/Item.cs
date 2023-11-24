using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;


public class Item : MonoBehaviour
{
    public float PickUpRange = 2f;
    public float PickUpSpeed = 1f;

    public float BobbingSpeed = 1f;
    public float BobbingHeight = 1f;
    private float originalY;

    [HideInInspector]
    public ItemData ItemData;
    private SpriteRenderer spriteRenderer;
    private bool pickedUp = false;

    private void Awake()
    {
        originalY = transform.position.y;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }    

    private void Update()
    {
        if (pickedUp)
        {
            if (Vector2.Distance(transform.position, Player.Instance.transform.position) < 0.1)
            {
                InventoryController.Instance.AddItem(ItemData);
                Destroy(gameObject);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.Instance.transform.position, Time.deltaTime * PickUpSpeed);
                var pos = transform.position - Player.Instance.transform.position;
                if (pos.y < 0)
                    pos = Player.Instance.transform.position - transform.position;

                transform.up = Vector3.RotateTowards(
                        transform.up,
                        pos,
                        Time.deltaTime * PickUpSpeed, 0.0f
                        );
            }
        }
        else
        {
            if (Player.Instance.InRange(transform.position, PickUpRange) && IsMouseOverObject())
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    pickedUp = true;
                }
                spriteRenderer.color = Color.gray;
            }
            else
            {
                spriteRenderer.color = Color.white;
            }

            // Item bobbing
            float yOffset = Mathf.Sin(Time.time * BobbingSpeed + originalY) * BobbingHeight;
            transform.position = new Vector3(transform.position.x, originalY + yOffset, transform.position.z);
        }
    }

    bool IsMouseOverObject()
    {
        // Cast a ray from the mouse position into the scene
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, 100.0f, LayerMask.GetMask("Item"));

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstruction : ResourceNode
{
    protected override void Update()
    {
        if (Player.Instance.InRange(transform.position, MiningRange) && IsMouseOverObject())
        {
            spriteRenderer.color = Color.gray;
            if (Input.GetKeyDown(KeyCode.Mouse0)) Mine();
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }

    bool IsMouseOverObject()
    {
        // Cast a ray from the mouse position into the scene
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, 100.0f, LayerMask.GetMask("Obstruction"));

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

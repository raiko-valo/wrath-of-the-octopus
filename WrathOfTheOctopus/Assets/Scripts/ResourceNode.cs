using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public ItemData Item;
    public int ItemDropAmount;
    public int ToolLevelRequired;

    [ContextMenu("DropItems")]
    public void DropItems()
    {
        for (int item = 0; item < ItemDropAmount; item++)
        {
            Item.Drop(transform.position);
        }
    }
}

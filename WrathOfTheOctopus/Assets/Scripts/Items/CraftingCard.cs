using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CraftingCard : MonoBehaviour
{
    public ItemData Item;

    private Button button;
    private Image image;
    private bool canCraft = false;

    private void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        Events.OnInventoryChanged += CanCraft;
    }

    private void OnDestroy()
    {
        Events.OnInventoryChanged -= CanCraft;
    }


    public void Craft()
    {
        if (canCraft)
        {
            foreach (ItemPair ingridient in Item.Recipe.Ingredients)
            {
                InventoryController.Instance.RemoveItem(ingridient.Item);
            }
            Events.AddItem(Item);
        }
    }

    void CanCraft()
    {
        canCraft = InventoryController.Instance.Inventory.CanCraft(Item);
        button.enabled = canCraft;
        if (canCraft)
        {
            image.color = new Color(1, 1, 1, 0.7f);
        }
        else
        {
            image.color = new Color(0.5f, 0.5f, 0.5f, 0.7f);
        }
    }
}

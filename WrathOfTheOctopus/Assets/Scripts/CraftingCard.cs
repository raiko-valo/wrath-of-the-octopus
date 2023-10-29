using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingCard : MonoBehaviour
{
    public ItemData Item;

    public void Craft()
    {
        foreach (ItemPair ingridient in Item.Recipe.Ingredients)
        {
            int sum = InventoryController.Instance.items.Where(item => item.Name == ingridient.Item.Name).Count();
            if (sum < ingridient.Ammount) return;
        }
        foreach (ItemPair ingridient in Item.Recipe.Ingredients)
        {
            InventoryController.Instance.RemoveItem(ingridient.Item);
        }
        Item.Drop(Player.Instance.transform.position);
    }
}

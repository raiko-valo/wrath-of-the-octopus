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
            int sum = 0;
            //int sum = InventoryController.Instance.inventory.Where(item => item.Value.Name == ingridient.Item.Name).Count();
            foreach (ItemData item in InventoryController.Instance.inventory.Values)
            {
                if (item.Name == ingridient.Item.Name)
                {
                    sum++;
                }
            }

            if (sum < ingridient.Ammount) return;
        }
        foreach (ItemPair ingridient in Item.Recipe.Ingredients)
        {
            InventoryController.Instance.RemoveItem(ingridient.Item);
        }
        InventoryController.Instance.AddItem(Item);
    }

}

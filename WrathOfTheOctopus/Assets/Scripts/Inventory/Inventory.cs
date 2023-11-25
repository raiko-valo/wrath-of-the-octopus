using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Inventory
{
    private Dictionary<int, ItemData> inventory = new();
    private List<InventorySlot> inventorySlots = new();
    private int selectedIndex = -1;

    public bool CanCraft(ItemData craftable)
    {
        foreach (ItemPair ingridient in craftable.Recipe.Ingredients)
        {
            int sum = 0;
            foreach (ItemData item in inventory.Values)
                if (item.Name == ingridient.Item.Name)
                    sum++;
            if (sum < ingridient.Ammount) return false;
        }
        return true;
    }

    #region Selected item
    public void ChangeSelected(int index)
    {
        selectedIndex = index;
    }

    public ItemData GetSelectedItem()
    {
        return inventory.ContainsKey(selectedIndex) ? inventory[selectedIndex] : null;
    }

    public int GetSelectedItemIndex()
    {
        return selectedIndex;
    }
    #endregion

    #region Slots
    public void AddSlot(InventorySlot slot)
    {
        inventorySlots.Add(slot);
    }

    public int SlotCount()
    {
        return inventorySlots.Count;
    }

    public InventorySlot GetLastSlot()
    {
        return inventorySlots[^1];
    }

    public InventorySlot GetSlotAt(int index)
    {
        return inventorySlots[index];
    }

    public void RemoveLastSlot()
    {
        inventorySlots.RemoveAt(inventorySlots.Count - 1);
    }
    #endregion

    #region Inventory

    public bool ContainsKey(int key)
    {
        return inventory.ContainsKey(key);
    }

    public ItemData GetLast()
    {
        return inventory[inventorySlots.Count - 1];
    }

    public ItemData GetAt(int index)
    {
        return inventory[index];
    }

    public void AddAt(int index, ItemData item)
    {
        inventory[index] = item;
        Events.InventoryChanged();
    }

    public void ChangeIndex(int from, int to)
    {
        if (from != to)
        {
            if (selectedIndex == from) Events.ChangeSelected(to);
            inventory[to] = inventory[from];
            inventory.Remove(from);
        }
    }

    public void RemoveLast()
    {
        inventory.Remove(inventorySlots.Count - 1);
        Events.InventoryChanged();
    }

    public void RemoveItem(ItemData item)
    {
        foreach (int key in inventory.Keys)
        {
            if (inventory[key].Name == item.Name)
            {
                RemoveAt(key);
                break;
            }
        }
    }

    public void RemoveAt(int index)
    {
        if (inventory.ContainsKey(index))
        {
            inventory.Remove(index);
            Events.InventoryChanged();
            GameObject child = inventorySlots[index].transform.GetChild(0).gameObject;
            if (child != null) GameObject.Destroy(child);
        }
    }
    #endregion
}

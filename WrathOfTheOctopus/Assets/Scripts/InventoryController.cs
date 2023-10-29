using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryController : MonoBehaviour
{
    public GameObject InventorySlotPrefab;
    public Health health;
    private List<GameObject> inventorySlots = new List<GameObject>();
    public List<ItemData> items = new List<ItemData>();
    private int space;
    public InventoryItem InventoryItemPrefab;
    public Text text;

    public static InventoryController Instance;

    private void Awake()
    {
        Instance = this;   
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < health.health; i++)
        {
            GameObject inventorySlot = Instantiate(InventorySlotPrefab, transform);
            inventorySlots.Add(inventorySlot);
        }

        if (items.Count > 0) InitialiseItems();
    }

    public void RemoveSlot()
    {
        Destroy(inventorySlots[0]);
        inventorySlots.RemoveAt(0);
        space--;
    }

    public void AddSlot()
    {
        inventorySlots.Add(Instantiate(InventorySlotPrefab, transform));
        space++;
    }

    public void AddItem(ItemData newItem)
    {
        if (space != 0)
        {
            foreach (GameObject slot in inventorySlots)
            {
                if (slot.transform.childCount == 0)
                {
                    items.Add(newItem);
                    InitialiseNewItem(newItem);
                    break;
                }
            }
        }
    }

    public void RemoveItem(ItemData item)
    {
        foreach (ItemData itemData in items)
        {
            if (itemData.Name == item.Name)
            {
                items.Remove(itemData);
                break;
            }
        }
        foreach (GameObject slot in inventorySlots)
        {
            GameObject child = slot.transform.GetChild(0).gameObject;
            if (child != null && child.GetComponent<InventoryItem>().item.Name == item.Name)
            {
                Destroy(child);
                break;
            }
        }
    }

    public void InitialiseItems()
    {
        int index = 0;
        foreach (ItemData item in items)
        {
            if (index <= health.health) break;
            if (inventorySlots[index].transform.childCount != 0) Destroy(inventorySlots[index].transform.GetChild(0));
            InventoryItem newItem = Instantiate(InventoryItemPrefab, inventorySlots[index].transform);
            newItem.InitialiseItem(item);
            newItem.text = text;
            index++;
        }
    }

    public void InitialiseNewItem(ItemData item)
    {
        for (int index = 0; index < space; index++)
        {
            if (inventorySlots[index].transform.childCount == 0)
            {
                InventoryItem newItem = Instantiate(InventoryItemPrefab, inventorySlots[index].transform);
                newItem.InitialiseItem(item);
                newItem.text = text;
                break;
            }
        }
    }
}

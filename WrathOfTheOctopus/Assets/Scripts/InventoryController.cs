using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public GameObject InventorySlotPrefab;
    public Health health;
    private List<GameObject> inventorySlots = new List<GameObject>();
    public List<ItemData> items = new List<ItemData>();
    private int space = 8;
    public InventoryItem InventoryItemPrefab;
    public Text text;

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
        inventorySlots.RemoveAt(0);
        space--;
    }

    public void AddSlot()
    {
        inventorySlots.Add(Instantiate(InventorySlotPrefab));
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
                    items.Add(newItem); break;
                }
            }
        }
    }

    public void InitialiseItems()
    {
        int index = 0;
        foreach (ItemData item in items)
        {
            if (inventorySlots[index].transform.childCount != 0) Destroy(inventorySlots[index].transform.GetChild(0));
            InventoryItem newItem = Instantiate(InventoryItemPrefab, inventorySlots[index].transform);
            newItem.InitialiseItem(item);
            newItem.text = text;
            index++;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public GameObject InventorySlotPrefab;
    public InventoryItem InventoryItemPrefab;
    public Text text;
    public List<ItemData> items = new();

    private readonly Stack<GameObject> inventorySlots = new();
    
    public static InventoryController Instance;

    private void Awake()
    {
        Instance = this;
        Events.OnAddHealth += OnAddHealth;
        Events.OnRemoveHealth += OnRemoveHealth;
    }

    private void OnDestroy()
    {
        Events.OnAddHealth -= OnAddHealth;
        Events.OnRemoveHealth -= OnRemoveHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Health.Instance.MaxHealth; i++)
        {
            GameObject inventorySlot = Instantiate(InventorySlotPrefab, transform);
            inventorySlot.GetComponent<InventorySlot>().Index = i;
            inventorySlots.Push(inventorySlot);
        }

        if (items.Count > 0) InitialiseItems();
    }

    void OnRemoveHealth(int amount)
    {
        ItemData[] inventoryItems = new ItemData[amount];
        for (int i = 0; i < amount; i++)
        {
            GameObject slot = inventorySlots.Peek();
            if (slot.transform.childCount != 0)
            {
                GameObject child = slot.transform.GetChild(0).gameObject;
                inventoryItems[i] = child.GetComponent<InventoryItem>().item;
                RemoveItem(inventoryItems[i]);
            }
            Destroy(inventorySlots.Pop());
        }
        for (int i = 0; i < amount; i++)
        {
            if (inventoryItems[i] == null) break;
            AddItem(inventoryItems[i]);
        }
    }

    void OnAddHealth(int amount)
    {
        for (int i = 0; i < amount; i++)
            inventorySlots.Push(Instantiate(InventorySlotPrefab, transform));
    }

    public void AddItem(ItemData newItem)
    {
        foreach (GameObject slot in inventorySlots)
        {
            if (slot.transform.childCount == 0)
            {
                items.Add(newItem);
                InventoryItem inventoryItem = Instantiate(InventoryItemPrefab, slot.transform);
                inventoryItem.InitialiseItem(newItem);
                inventoryItem.text = text;
                return;
            }
        }
        newItem.Drop(Player.Instance.transform.position);
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
            if (slot.transform.childCount != 0)
            {
                GameObject child = slot.transform.GetChild(0).gameObject;
                if (child != null && child.GetComponent<InventoryItem>().item.Name == item.Name)
                {
                    Destroy(child);
                    break;
                }
            }
        }
    }

    public void InitialiseItems()
    {
        int index = 0;
        foreach (GameObject slot in inventorySlots)
        {
            if (index >= items.Count) break;
            if (slot.transform.childCount != 0) Destroy(slot.transform.GetChild(0));
            InventoryItem newItem = Instantiate(InventoryItemPrefab, slot.transform);
            newItem.InitialiseItem(items[index]);
            newItem.text = text;
            index++;
        }
    }
}

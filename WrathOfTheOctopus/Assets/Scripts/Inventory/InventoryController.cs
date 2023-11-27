using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public InventorySlot InventorySlotPrefab;
    public InventoryItem InventoryItemPrefab;
    public Text text;
    public AudioClipGroup audioClipPickup;

    public List<ItemData> StartingInventory = new();
    private Inventory inventory = new();
    public Inventory Inventory => inventory;

    public static InventoryController Instance;

    private void Awake()
    {
        Instance = this;
        Events.OnAddHealth += OnAddHealth;
        Events.OnRemoveHealth += OnRemoveHealth;
        Events.OnChangeSelected += OnChangeSelected;
        Events.OnAddItem += OnAddItem;
        Events.OnGetInventoryWheelSize += OnGetInventoryWheelSize;
    }

    private void OnDestroy()
    {
        Events.OnAddHealth -= OnAddHealth;
        Events.OnRemoveHealth -= OnRemoveHealth;
        Events.OnChangeSelected -= OnChangeSelected;
        Events.OnAddItem -= OnAddItem;
        Events.OnGetInventoryWheelSize += OnGetInventoryWheelSize;
    }

    void Start()
    {
        OnAddHealth(Health.Instance.MaxHealth);
        if (StartingInventory.Count > 0) InitialiseStartingItems();
    }

    void OnChangeSelected(int index)
    {
        inventory.ChangeSelected(index);
    }

    void OnRemoveHealth(int amount)
    {
        for (int i = 0; i < amount; i++)
        {   
            if (inventory.ContainsKey(inventory.SlotCount() - 1))
            {
                OnAddItem(inventory.GetLast());
                inventory.RemoveLast();
            }
            Destroy(inventory.GetLastSlot().gameObject);
            inventory.RemoveLastSlot();
        }
    }

    void OnAddHealth(int amount)
    {
        int maxIndexBefore = inventory.SlotCount();
        for (int i = maxIndexBefore; i < maxIndexBefore + amount; i++)
        {
            if (Health.Instance.MaxHealth == i) break;
            InventorySlot inventorySlot = Instantiate(InventorySlotPrefab, transform);
            inventorySlot.Index = i;
            inventory.AddSlot(inventorySlot);
        }
    }

    public void OnAddItem(ItemData newItem)
    {
        for (int i = 0; i < inventory.SlotCount(); i++)
        {
            if (inventory.GetSlotAt(i).transform.childCount == 0)
            {
                inventory.AddAt(i, newItem);
                InventoryItem inventoryItem = Instantiate(InventoryItemPrefab, inventory.GetSlotAt(i).transform);
                inventoryItem.InitialiseItem(newItem);
                inventoryItem.text = text;
                audioClipPickup.Play();
                return;
            }
        }
        newItem.Drop(Player.Instance.transform.position);
    }

    public void RemoveItem(ItemData item)
    {
        inventory.RemoveItem(item);
    }

    public void RemoveItemAt(int index)
    {
        inventory.RemoveAt(index);
    }

    public ItemData GetSelected()
    {
        return inventory.GetSelectedItem();
    }

    public int GetSelectedIndex()
    {
        return inventory.GetSelectedItemIndex();
    }

    public void ChangeIndex(int from, int to)
    {
        inventory.ChangeIndex(from, to);
    }

    public void InitialiseStartingItems()
    {
        for (int i = 0; i < inventory.SlotCount(); i++)
        {
            if (i >= StartingInventory.Count || StartingInventory[i] == null) break;
            if (inventory.GetSlotAt(i).transform.childCount != 0) 
                Destroy(inventory.GetSlotAt(i).transform.GetChild(0).gameObject);
            inventory.AddAt(i, StartingInventory[i]);
            InventoryItem newItem = Instantiate(InventoryItemPrefab, inventory.GetSlotAt(i).transform);
            newItem.InitialiseItem(inventory.GetAt(i));
            newItem.text = text;
        }
    }

    public float OnGetInventoryWheelSize()
    {
        return transform.GetComponent<RectTransform>().rect.height / 2;
    }
}
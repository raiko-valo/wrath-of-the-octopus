using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryController : MonoBehaviour
{
    public InventorySlot InventorySlotPrefab;
    public InventoryItem InventoryItemPrefab;
    public Text text;
    public AudioClipGroup audioClipPickup;

    public List<ItemData> StartingInventory = new();

    //[HideInInspector]
    public  Dictionary<int, ItemData> inventory = new();
    public  List<InventorySlot> inventorySlots = new();
    private int selectedIndex = -1;

    public static InventoryController Instance;

    private void Awake()
    {
        Instance = this;
        Events.OnAddHealth += OnAddHealth;
        Events.OnRemoveHealth += OnRemoveHealth;
        Events.OnChangeSelected += OnChangeSelected;
    }

    private void OnDestroy()
    {
        Events.OnAddHealth -= OnAddHealth;
        Events.OnRemoveHealth -= OnRemoveHealth;
        Events.OnChangeSelected -= OnChangeSelected;
    }

    void Start()
    {
        OnAddHealth(Health.Instance.MaxHealth);
        if (StartingInventory.Count > 0) InitialiseStartingItems();
    }

    void OnChangeSelected(int index)
    {
        selectedIndex = index;
    }

    void OnRemoveHealth(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            
            if (inventory.ContainsKey(inventorySlots.Count - 1))
            {
                AddItem(inventory[inventorySlots.Count - 1]);
                inventory.Remove(inventorySlots.Count - 1);
            }
            Destroy(inventorySlots[^1].gameObject);
            inventorySlots.RemoveAt(inventorySlots.Count-1);
        }
    }

    void OnAddHealth(int amount)
    {
        int maxIndexBefore = inventorySlots.Count;
        for (int i = maxIndexBefore; i < maxIndexBefore + amount; i++)
        {
            InventorySlot inventorySlot = Instantiate(InventorySlotPrefab, transform);
            inventorySlot.Index = i;
            inventorySlots.Add(inventorySlot);
        }
    }

    public void AddItem(ItemData newItem)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].transform.childCount == 0)
            {
                inventory[i] = newItem;
                InventoryItem inventoryItem = Instantiate(InventoryItemPrefab, inventorySlots[i].transform);
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
        foreach (int key in inventory.Keys) 
        {
            if (inventory[key].Name == item.Name)
            {
                RemoveItemAt(key);
                break;
            }
        }
    }

    public void RemoveItemAt(int index)
    {
        if (inventory.ContainsKey(index))
        {
            inventory.Remove(index);
            GameObject child = inventorySlots[index].transform.GetChild(0).gameObject;
            if (child != null) Destroy(child);
        }
    }

    public ItemData GetSelected()
    {
        return inventory.ContainsKey(selectedIndex)? inventory[selectedIndex] : null;
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

    public void InitialiseStartingItems()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (i >= StartingInventory.Count || StartingInventory[i] == null) break;
            if (inventorySlots[i].transform.childCount != 0) Destroy(inventorySlots[i].transform.GetChild(0).gameObject);
            inventory[i] = StartingInventory[i];
            InventoryItem newItem = Instantiate(InventoryItemPrefab, inventorySlots[i].transform);
            newItem.InitialiseItem(inventory[i]);
            newItem.text = text;
        }
    }
}
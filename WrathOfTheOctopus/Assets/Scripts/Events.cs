using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Events
{
    public static event Action<int> OnAddHealth;
    public static void AddHealth(int value) => OnAddHealth?.Invoke(value);
    public static event Action<int> OnRemoveHealth;
    public static void RemoveHealth(int value) => OnRemoveHealth?.Invoke(value);

    public static event Action OnDeath;
    public static void Died() => OnDeath?.Invoke();


    public static event Action<int> OnChangeSelected;
    public static void ChangeSelected(int value) => OnChangeSelected?.Invoke(value);


    public static event Func<float> OnGetInventoryWheelSize;
    public static float GetInventoryWheelSize() => OnGetInventoryWheelSize?.Invoke() ?? 0;


    public static event Action<ItemData> OnAddItem;
    public static void AddItem(ItemData value) => OnAddItem?.Invoke(value);

    public static event Action OnInventoryChanged;
    public static void InventoryChanged() => OnInventoryChanged?.Invoke();
}

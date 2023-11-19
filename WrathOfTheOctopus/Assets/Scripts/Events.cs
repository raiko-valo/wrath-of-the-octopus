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
}

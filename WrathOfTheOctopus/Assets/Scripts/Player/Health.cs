using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public static Health Instance;
    public int MaxHealth;
    public int CurrentHealth;
    public Text count;

    private void Awake()
    {
        Instance = this;
        Events.OnAddHealth += OnAddHealth;
        Events.OnRemoveHealth += OnRemoveHealth;
        Events.OnDeath += OnDeath;
    }

    private void OnDestroy()
    {
        Events.OnAddHealth -= OnAddHealth;
        Events.OnRemoveHealth -= OnRemoveHealth;
        Events.OnDeath -= OnDeath;
    }

    private void Start()
    {
        count.text = CurrentHealth.ToString();
    }

    void OnRemoveHealth(int amount)
    {
        CurrentHealth -= Mathf.Max(0, amount);
        count.text = CurrentHealth.ToString();

        if (CurrentHealth <= 0) Events.Died();
    }

    void OnAddHealth(int amount)
    {
        CurrentHealth += amount;
        count.text = CurrentHealth.ToString();
    }

    void OnDeath()
    {
        Events.AddHealth(MaxHealth);
    }

    [ContextMenu("RemoveHealth")]
    public void TestRemove()
    {
        Events.RemoveHealth(2);
    }

    [ContextMenu("AddHealth")]
    public void TestAdd()
    {
        Events.AddHealth(2);
    }
}

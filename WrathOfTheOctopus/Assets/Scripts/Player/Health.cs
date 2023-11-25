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
        print(CurrentHealth);
        count.text = CurrentHealth.ToString();
        print(count.text);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Events.RemoveHealth(1);
        }
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
        Events.RemoveHealth(1);
    }

    [ContextMenu("AddHealth")]
    public void TestAdd()
    {
        Events.AddHealth(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public static Health Instance;
    public int health;
    public Text count;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        count.text = health.ToString();
    }

    public void RemoveHealth()
    {
        health--;
        InventoryController.Instance.RemoveSlot();
        count.text = health.ToString();
        if (health == 0)
        {
            Player.Instance.RespawnPlayer();
            for (int i = 0; i < 8; i++) 
            { 
                AddHealth();
                InventoryController.Instance.AddSlot();
            }
        }
    }

    public void AddHealth()
    {
        health++;
        count.text = health.ToString();
    }
}

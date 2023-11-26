using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryVisualiser : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject GameMenu;

    private bool active = false;
    private bool menuActive = false;

    private void Awake()
    {
        Inventory.SetActive(false);
        GameMenu.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Inventory.SetActive(!active);
            active = !active;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameMenu.SetActive(!menuActive);
            menuActive = !menuActive;
        }
    }
}

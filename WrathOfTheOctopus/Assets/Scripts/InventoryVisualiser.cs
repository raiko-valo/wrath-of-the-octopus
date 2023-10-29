using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryVisualiser : MonoBehaviour
{
    public bool active = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("pressed");
            if (!active) active = true;
            else active = false;
        }

        if (active) gameObject.SetActive(true);
        else gameObject.SetActive(false);
    }
}

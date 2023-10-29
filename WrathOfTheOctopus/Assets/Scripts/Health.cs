using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public Text count;
    // Start is called before the first frame update
    private void Start()
    {
        count.text = health.ToString();
    }

    public void RemoveHealth()
    {
        health--;
        count.text = health.ToString();
    }

    public void AddHealth()
    {
        health++;
        count.text = health.ToString();
    }
}

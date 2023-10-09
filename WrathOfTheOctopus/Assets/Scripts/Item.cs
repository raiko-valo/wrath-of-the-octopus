using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData ItemData;

    private void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = ItemData.Sprite;
    }
}

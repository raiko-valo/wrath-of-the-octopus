using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

[CreateAssetMenu(menuName = "Game/Item")]
public class ItemData : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Sprite;
    public GameObject itemPrefab;

    public CraftingRecipe Recipe;

    private bool consumable = false;

    public void Drop(Vector3 location)
    {        
        GameObject gm = Instantiate(itemPrefab, location, Quaternion.identity);
        SpriteRenderer sr = gm.GetComponent<SpriteRenderer>();
        sr.sprite = Sprite;
        sr.size = new Vector2(0.5f, 0.5f);
        gm.GetComponent<Item>().ItemData = this;
    }

    public bool GetConsumable()
    {
        return consumable;
    }

    public void Consume()
    {
        return;
    }
}

[Serializable]
public class CraftingRecipe
{
    public List<ItemPair> Ingredients;
    public int Ammount;
}

[Serializable]
public class ItemPair
{
    public ItemData Item;
    public int Ammount;
}
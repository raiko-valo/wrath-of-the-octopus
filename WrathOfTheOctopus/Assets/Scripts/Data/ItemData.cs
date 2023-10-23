using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Item")]
public class ItemData : ScriptableObject
{
    public string Name;
    public string Description;
    public GameObject Prefab;

    public CraftingRecipe Recipe;

    public void Drop(Vector3 location)
    {
        GameObject gameObject = Instantiate(Prefab, location, Quaternion.identity);
        gameObject.GetComponent<Item>().ItemData = this;
    }
}

[Serializable]
public class CraftingRecipe
{
    public List<ItemPair> Ingredients;
    public ItemPair Result;
}

[Serializable]
public class ItemPair
{
    public ItemData Item;
    public int Ammount;
}
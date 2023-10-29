using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class ItemSten : ScriptableObject
{
    [Header("Only Gameplay")]
    public ItemType type;
    public ActionType actionType;
    [Header("Only UI")]
    public bool stackable = false;
    [Header("Both")]
    public Sprite image;

    public enum ItemType
    {
        Consumable,
        Crafting,
        Equip
    }

    public enum ActionType
    {
        Consume,
        Equip,
        None
    } 
}

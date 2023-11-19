using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Game/CraftingList")]
public class CraftingData : ScriptableObject
{
    public List<ItemData> Items;
}

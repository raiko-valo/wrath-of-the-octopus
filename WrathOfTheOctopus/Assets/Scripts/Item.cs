using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Item")]
public class Item : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Sprite;
}


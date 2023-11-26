using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/HealthBuff")]
public class HealthBuff : ItemData
{
    public int AddedHealth;
    public AudioClipGroup AudioClipConsume;

    private bool consumable = true;

    public new void Consume()
    {
        Events.AddHealth(AddedHealth);
        AudioClipConsume.Play();
    }

    public new bool GetConsumable()
    {
        return consumable;
    }
}
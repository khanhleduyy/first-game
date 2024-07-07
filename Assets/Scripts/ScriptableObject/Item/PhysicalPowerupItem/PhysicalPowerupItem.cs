using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PhysicalPowerupItem : ScriptableObject
{
    public abstract void Apply(GameObject player);

    public int dropChance;
    public Color color;
    public string ItemName;
    public rarity rarity;
    public Sprite iconSprite;
}
public enum rarity
{
    common,
    uncommon,
    rare,
    epic,
    legendary,
}



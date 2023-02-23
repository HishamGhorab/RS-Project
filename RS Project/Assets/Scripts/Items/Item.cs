using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {Material, Rune, Weapon, Shield, Projectile, Head, Torso, Legs, Boots, Glove, Cape, Amulet, Ring}

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public bool stackable;

    public bool NoModel; //needs to change
    public GameObject model;
}


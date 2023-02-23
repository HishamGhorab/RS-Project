using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    public Item item;

    [Header("Weapon attributes")] 
    public int damage;
    public int crit;
    public int pierce;

    public List<int> mods;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    [SerializeField] private int CombatLevel;
    [SerializeField] private int MiningLevel;
    [SerializeField] private int WoodcuttingLevel;

    public static Dictionary<string, int> LevelDictionary;

    public void Start()
    {
        //todo: figure out a better way for this because skills wont update when u level in the variables above
        LevelDictionary = new Dictionary<string, int>();
        
        LevelDictionary.Add("CombatLevel", CombatLevel);
        LevelDictionary.Add("MiningLevel", MiningLevel);
        LevelDictionary.Add("WoodcuttingLevel", WoodcuttingLevel);
    }
}

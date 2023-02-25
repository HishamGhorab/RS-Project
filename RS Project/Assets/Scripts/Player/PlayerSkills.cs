using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    [SerializeField] private Level CombatLevel;
    [SerializeField] private Level MiningLevel;
    [SerializeField] private Level WoodcuttingLevel;
    
    public Dictionary<string, Level> LevelDictionary;

    private void Awake()
    {
        Level.InitExperienceDictionary();
    }

    public void Start()
    {
        //todo: figure out a better way for this because skills wont update when u level in the variables above
        LevelDictionary = new Dictionary<string, Level>();
        
        LevelDictionary.Add("CombatLevel", CombatLevel);
        LevelDictionary.Add("MiningLevel", MiningLevel);
        LevelDictionary.Add("WoodcuttingLevel", WoodcuttingLevel);
    }
}

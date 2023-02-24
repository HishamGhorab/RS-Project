using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        Level level;
    }
}

[Serializable]
public class Level
{
    public int level;
    public int experience;
    public int experienceToLevel;

    public static Dictionary<int, int> experienceScaling;

    public int Experience
    {
        get => experience;
        set
        {
            experience = value;
            //if(experience >= )
        }
    }
    private Level()
    {
        Round(23);
        experienceScaling.Add(10, 100);
        experienceScaling.Add(20, 200);
        experienceScaling.Add(30, 300);
        experienceScaling.Add(40, 400);
        experienceScaling.Add(50, 500);
        experienceScaling.Add(60, 600);
        experienceScaling.Add(70, 700);
        experienceScaling.Add(80, 800);
        experienceScaling.Add(99, 900);
    }

    public int Round(int number)
    {
        Debug.Log(((int)(number / 10)) * 10);
        return ((int)(number / 10)) * 10;
    }
}

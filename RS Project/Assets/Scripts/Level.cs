using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
    [NonSerialized] public static Dictionary<int, int> experienceDictionary = new Dictionary<int, int>();
    
    public int level = 1;
    [SerializeField] private int experience = 0;
    public int experienceToLevel = 100;

    public int Experience
    {
        get => experience;
        set
        { 
            experience = value;
            CheckIfLeveled();
        }
    }

    public static void InitExperienceDictionary()
    {
        AddExp(1, 100);
        AddExp(2, 200);
        AddExp(3, 400);
        AddExp(4, 800);
        AddExp(5, 1000);
        AddExp(6, 2000);
        AddExp(7, 3000);
        AddExp(8, 4000);
        AddExp(9, 5000);
        AddExp(10, 10000);

        void AddExp(int level, int neededExp)
        {
            experienceDictionary.Add(level, neededExp);
        }
    }

    public void CheckIfLeveled()
    {
        if (experience >= experienceToLevel)
        {
            experienceToLevel = GetNextExpToLvl(LevelUp());
        }
    }
    
    int LevelUp()
    {
        return level++;
    }

    int GetNextExpToLvl(int level)
    {
        return experienceDictionary[level];
    }
}

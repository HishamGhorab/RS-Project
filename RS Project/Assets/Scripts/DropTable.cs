using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Items;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class DropTable
{
    public List<Table> dropTables;

    public List<Item> RollTables()
    {
        List<Item> rolls = new List<Item>();
        
        for (int i = 0; i < dropTables.Count; i++)
        {
            rolls.Add(dropTables[i].Roll());
        }

        return rolls;
    }

    [Serializable]
    public struct Table
    {
        public List<Loot> tableLoot;

        [Serializable]
        public struct Loot
        {
            public Item item;
            public int odds;
        }
        
        public Item Roll()
        {
            Item item = null;

            int highestOutOf = 0;
            
            for (int i = 0; i < tableLoot.Count; i++)
            {
                Loot loot = tableLoot[i];
                int randomNum = Random.Range(1, loot.odds);
                
                if (randomNum == 1)
                {
                    if (loot.odds > highestOutOf)
                    {
                        highestOutOf = loot.odds;
                        item = loot.item;
                    }   
                }
            }

            return item;
        }
    }
}

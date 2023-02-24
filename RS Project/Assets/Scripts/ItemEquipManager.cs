using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEquipManager : MonoBehaviour
{
    //When an item has been equipped, i should spawn this item object on the player 
    public Transform handPosition;

    public Dictionary<string, GameObject> storedItems = new Dictionary<string, GameObject>();

    private void OnEnable()
    {
        PlayerInventory.onItemEquip += EquipItem;
    }

    private void OnDisable()
    {
        PlayerInventory.onItemEquip -= EquipItem;
    }

    public void UpdateModelsInEquipList(PlayerInventory playerInventory)
    {
        for (int i = 0; i < playerInventory.inventory.Length; i++)
        {
            Item item = playerInventory.inventory[i].item;
            
            //if slot is empty
            if(item == null)
                continue;
            
            //if slot is not a weapon
            if(item.itemType != ItemType.Weapon)
                continue;
            
            //temporary solution all items should have a model
            if (item.NoModel)
                continue;
            
            //If has already been added
            if (storedItems.ContainsKey(item.itemName))
                continue;
            
            GameObject itemModel = Instantiate(item.model, handPosition);
            itemModel.SetActive(false);
            
            storedItems.Add(item.itemName, itemModel);
        }
    }

    public void EquipItem(Item _item)
    {
        //this needs to improve in performance
        //this needs to work generically and not just for weapons
        foreach (GameObject model in storedItems.Values)
        {
            model.SetActive(false);
        }
        
        //temp
        if (_item.NoModel)
            return;
        
        storedItems[_item.itemName].SetActive(true);
    }
}

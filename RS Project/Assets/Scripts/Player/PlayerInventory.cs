using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int maxStackValue;
    [SerializeField] private InventoryUI inventoryUI;

    public EquipSlot[] Equips = new EquipSlot[6];

    public InventorySlot[] inventory = new InventorySlot[28];

    private ItemEquipManager itemEquipManager;
    
    public delegate void OnItemEquip(Item _item);
    public static event OnItemEquip onItemEquip;

    private void Start()
    {
        itemEquipManager = GetComponent<ItemEquipManager>();
        itemEquipManager.UpdateModelsInInventory(this);
    }

    public void AddToInventory(Item _item)
    {
        if (_item.stackable)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (i == inventory.Length - 1)
                {
                    for (int j = 0; j < inventory.Length; j++)
                    {
                        //find an empty slot
                        if (inventory[j].item != null)
                            continue;

                        inventory[j].item = _item;
                        inventory[j].quantity++;
                        UpdateInventoryUI();
                        return;
                    }  
                }
                
                if (inventory[i].item == null)
                    continue;
                
                if (inventory[i].item.itemName == _item.itemName && inventory[i].quantity < maxStackValue)
                {
                    inventory[i].item = _item;
                    inventory[i].quantity++;
                    UpdateInventoryUI();
                    return;   
                }
            }  
        }
        else
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                //find an empty slot
                if (inventory[i].item != null)
                    continue;

                inventory[i].item = _item;
                inventory[i].quantity++;
                UpdateInventoryUI();
                return;
            }  
        }
    }

    public void RemoveFromInventory(int _slot)
    {
        inventory[_slot - 1].item.itemName = "";
        UpdateInventoryUI();
    }

    public bool SearchItem(Item _item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].item == null)
                return false;
            
            if (inventory[i].item.itemName == _item.itemName)
                return true;
        }
        return false;
    }

    public void EquipItem(int invSlot)
    {
        Item item = inventory[invSlot].item;

        if (item == null)
        {
            Debug.Log($"No item to equip.");
            return;
        }
        
        for (int i = 0; i < Equips.Length; i++)
        {
            //Check if pressing empty item slot
            if (item.itemType != Equips[i].ItemType)
                continue;
            
            //Replacing
            if (Equips[i].slot.item != null)
            {
                Item tempItem = Equips[i].slot.item;
                Debug.Log($"You replaced {tempItem} for {Equips[i].slot.item}");

                inventory[invSlot].item = Equips[i].slot.item;
                
                Equips[i].slot.quantity = 1;
                Equips[i].slot.item = item;
                UpdateInventoryUI();
                onItemEquip.Invoke(item);
                
                return;
            }
            
            //Equipping 
            Debug.Log($"You equipped {Equips[i].slot.item}");
            Equips[i].slot.quantity = 1;
            Equips[i].slot.item = item;
            inventory[invSlot].item = null;
            UpdateInventoryUI();
            onItemEquip.Invoke(item);
            return;
        }
        
        Debug.Log($"{item.itemName} cant be equipped");
    }

    void UpdateInventoryUI()
    {
        inventoryUI.UpdateInventoryUI();
        itemEquipManager.UpdateModelsInInventory(this);
    }
}

[Serializable]
public class InventorySlot
{
    public Item item;
    public int quantity;
}

[Serializable]
public class EquipSlot
{
    public ItemType ItemType;
    public InventorySlot slot;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public GameObject inventorySlotUi;

    private List<GameObject> inventorySlots = new List<GameObject>();

    private void Start()
    {
        SpawnSlots();
    }

    void SpawnSlots()
    {
        //spawn all slots and add them to a list
        for (int i = 0; i < playerInventory.inventory.Length; i++)
        {
            GameObject slot = Instantiate(inventorySlotUi, gameObject.transform, true);
            inventorySlots.Add(slot);
        }
        
        //name the slots according to what u have in the inventory
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        //name the slots according to what u have in the inventory
        for (int i = 0; i < playerInventory.inventory.Length; i++)
        {
            ItemSlotUi itemSlotUi = inventorySlots[i].GetComponent<ItemSlotUi>();
            itemSlotUi.ItemName.text = "Empty";
            itemSlotUi.ItemQuantity.text = 0.ToString();
            itemSlotUi.Slot = i;

            if(playerInventory.inventory[i].item == null)
                continue;

            itemSlotUi.ItemName.text = playerInventory.inventory[i].item.itemName;
            itemSlotUi.ItemQuantity.text = playerInventory.inventory[i].quantity.ToString();
        }
    }
}

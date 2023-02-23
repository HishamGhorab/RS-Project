using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemSlotUi : MonoBehaviour
{
    private TextMeshProUGUI itemName;
    private TextMeshProUGUI itemQuantity;

    private PlayerInventory playerInventory;

    private int slot;
    public TextMeshProUGUI ItemName
    {
        get => itemName;
        set => itemName = value;
    }

    public TextMeshProUGUI ItemQuantity
    {
        get => itemQuantity;
        set => itemQuantity = value;
    }

    public int Slot
    {
        get => slot;
        set => slot = value;
    }
    void Awake()
    {
        itemName = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        itemQuantity = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        playerInventory = GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();
    }

    public void EquipItemOnSlot()
    {
        playerInventory.EquipItem(slot);
    }
}

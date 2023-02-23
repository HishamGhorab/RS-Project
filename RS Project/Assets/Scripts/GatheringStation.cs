using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
[RequireComponent(typeof(Outline))]
public class GatheringStation : MonoBehaviour
{
    [Range(0, 99)] [SerializeField] 
    private int levelToInteract;

    public string trainedSkillName;

    [SerializeField] private Item itemToInteract;
    
    [SerializeField] private int chanceToDeplete;
    
    //[Range(0, 100)] [SerializeField] 
    //private float percentToReward;
    
    [SerializeField] private DropTable dropTable;
    
    public bool interacting;
    private float nextSecond;

    private PlayerInventory interactingPlayerInv;
    private int interactingPlayerLevel;

    private void Update()
    {
        if (interacting && Time.time >= nextSecond)
        {
            if (DidDeplete())
            {
                OnDeplete();
                return;
            }
            
            Interact(interactingPlayerInv, interactingPlayerLevel);
            nextSecond = Time.time + 1;
        }
    }

    public void StartInteracting(PlayerInventory playerInventory, int level)
    {
        interactingPlayerInv = playerInventory;
        interactingPlayerLevel = level;
        
        if (level < levelToInteract)
        {
            Debug.Log("You dont have the required level to interact with this");
            StopInteracting();
            return;
        }

        if (!playerInventory.SearchItem(itemToInteract))
        {
            Debug.Log("You dont have the required tool to interact with this");
            StopInteracting();
            return;
        }

        if (playerInventory.inventory[playerInventory.inventory.Length - 1].item != null)
        {
            Debug.Log("Your Inventory is full!");
            StopInteracting();
            return;
        }
        
        nextSecond = Time.time + 1;
        interacting = true;
    }

    public void StopInteracting()
    {
        interactingPlayerInv = null;
        interactingPlayerLevel = -1;
        
        GetComponent<InteractableObject>().OnDeselected();

        interacting = false;
    }

    public void Interact(PlayerInventory playerInventory, int level)
    {
        Result(playerInventory);
    }

    public void Result(PlayerInventory playerInventory)
    {
        List<Item> rolls = dropTable.RollTables();
        for (int i = 0; i < rolls.Count; i++)
        {
            Debug.Log(rolls[i]);
            
            if (rolls[i] == null)
                continue;
            
            playerInventory.AddToInventory(rolls[i]);
        }
    }

    public bool DidDeplete()
    {
        int randomNum = Random.Range(1, chanceToDeplete);
        
        if (randomNum == 1)
            return true;
        
        return false;
    }

    public void OnDeplete()
    {
        StopInteracting();
        Destroy(gameObject);
    }
}
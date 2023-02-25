using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using Random = UnityEngine.Random;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
[RequireComponent(typeof(Outline))]
public class GatheringStation : MonoBehaviour
{
    [Range(1, 99)] [SerializeField] 
    private int levelToInteract;

    public string trainedSkillName;

    [SerializeField] private Item itemToInteract;
    [SerializeField] private int chanceToDeplete;
    [SerializeField] private int expReward;

    [SerializeField] private DropTable dropTable;
    
    public bool interacting;
    
    private GameObject interactingPlayer;
    private float nextSecond;


    private void Update()
    {
        if (interacting && Time.time >= nextSecond)
        {
            if (DidDeplete())
            {
                OnDeplete();
                return;
            }
            
            RewardPlayer();
            nextSecond = Time.time + 1;
        }
    }

    public void StartInteracting(GameObject _interactingPlayer)
    {
        interactingPlayer = _interactingPlayer;
        
        PlayerInventory playerInventory = interactingPlayer.GetComponent<PlayerInventory>();
        Level level = interactingPlayer.GetComponent<PlayerSkills>().LevelDictionary[trainedSkillName + "Level"];

        if (level.level < levelToInteract)
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
        interactingPlayer = null;

        GetComponent<InteractableObject>().OnDeselected();

        interacting = false;
    }

    public void RewardPlayer()
    {
        PlayerInventory playerInventory = interactingPlayer.GetComponent<PlayerInventory>();
        Level level = interactingPlayer.GetComponent<PlayerSkills>().LevelDictionary[trainedSkillName + "Level"];

        level.Experience += expReward;
        LootResult(playerInventory);
    }

    public void LootResult(PlayerInventory playerInventory)
    {
        List<Item> rolls = dropTable.RollTables();
        for (int i = 0; i < rolls.Count; i++)
        {
            //Debug.Log(rolls[i]);
            
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
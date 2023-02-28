using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    //this is the head class of the player. Referencing and communication from other object and sources should happen through this
    [HideInInspector] public PlayerToMousePos PlayerToMousePosComponent;
    [HideInInspector] public PlayerSkills PlayerSkillsComponent;
    [HideInInspector] public Interact InteractComponent;
    [HideInInspector] public Distance DistanceComponent;
    [HideInInspector] public PlayerInventory PlayerInventoryComponent;
    [HideInInspector] public ItemEquipManager ItemEquipManagerComponent;
    [HideInInspector] public SelectManager SelectManagerComponent;

    [HideInInspector] public Animator AnimatorComponent;
    [HideInInspector] public NavMeshAgent AgentComponent;
    
    public PlayerAnimation playerAnimation;
    public Transform handPosition;

    private void Awake()
    {
        PlayerToMousePosComponent = GetComponent<PlayerToMousePos>();
        PlayerSkillsComponent = GetComponent<PlayerSkills>();
        InteractComponent = GetComponent<Interact>();
        DistanceComponent = GetComponent<Distance>();
        PlayerInventoryComponent = GetComponent<PlayerInventory>();
        ItemEquipManagerComponent = GetComponent<ItemEquipManager>();
        SelectManagerComponent = GetComponent<SelectManager>();

        AnimatorComponent = playerAnimation.gameObject.GetComponent<Animator>();
        AgentComponent = GetComponent<NavMeshAgent>();
    }
}

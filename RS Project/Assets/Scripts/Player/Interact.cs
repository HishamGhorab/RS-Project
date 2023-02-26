using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private GatheringStation station;
    
    private PlayerInventory playerInventory;

    private SelectManager selectManager;

    private GatheringStation gStationInteracting;

    public bool interacting = false;

    public static Action OnGatheringEventStart;
    public static Action OnGatheringEventEnd;

    private void OnEnable()
    {
        PlayerToMousePos.onMeleePosReached += OnInteractStart;
        PlayerToMousePos.onGroundPosPressed += CheckIfInteractionEnd;
    }
    
    private void OnDisable()
    {
        PlayerToMousePos.onMeleePosReached -= OnInteractStart;
        PlayerToMousePos.onGroundPosPressed -= CheckIfInteractionEnd;
    }


    private void Start()
    {
        playerInventory = GetComponent<PlayerInventory>();
        selectManager = GetComponent<SelectManager>();
    }

    public void OnInteractStart()
    {
        interacting = true;

        InteractableObject iObject = selectManager.highlightedObject;
        
        if (iObject.interactionType == InteractableObject.InteractionType.Gathering)
        {
            gStationInteracting = iObject.GetComponent<GatheringStation>();
            GatherInteractStart(gStationInteracting);
        }
        else
        {
            CombatInteractStart();
        }
    }
    
    public void OnInteractTick()
    {
        
    }

    public void CheckIfInteractionEnd()
    {
        if (interacting)
        {
            OnGatheringStop();
            interacting = false;
        }
    }

    public void GatherInteractStart(GatheringStation station)
    {
        station.StartInteracting(gameObject);
        OnGatheringEventStart.Invoke();
    }
    
    public void OnGatheringStop()
    {
        if (!gStationInteracting)
            return;
        
        Debug.Log("hi");
        
        gStationInteracting.StopInteracting();
        OnGatheringEventEnd.Invoke();
        gStationInteracting = null;
    }
    
    public void CombatInteractStart()
    {
        
    }
}

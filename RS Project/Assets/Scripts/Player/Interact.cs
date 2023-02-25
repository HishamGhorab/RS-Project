using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private GatheringStation station;
    
    private PlayerInventory playerInventory;

    private SelectManager selectManager;

    private GatheringStation gStationInteracting;

    public bool interacting = false;

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
    
    public void OnInteractEnd()
    {
        if (!gStationInteracting)
            return;
        
        gStationInteracting.StopInteracting();
        gStationInteracting = null;
    }

    public void CheckIfInteractionEnd()
    {
        if (interacting)
        {
            OnInteractEnd();
            interacting = false;
        }
    }

    public void GatherInteractStart(GatheringStation station)
    {
        station.StartInteracting(gameObject);
    }
    
    public void CombatInteractStart()
    {
        
    }
}

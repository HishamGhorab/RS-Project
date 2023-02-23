using System;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerToMousePos : MonoBehaviour
{
    private Distance distance;
    
    private NavMeshAgent agent;
    
    private Vector3 mousePosition;
    private Vector3 selectedPosition;

    public bool movingToSelected = false;
    
    public delegate void OnMeleePosReached();
    public static event OnMeleePosReached onMeleePosReached;
    
    public delegate void OnGroundPosPressed();
    public static event OnGroundPosPressed onGroundPosPressed;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        distance = GetComponent<Distance>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        Camera cam = Camera.main;
        
        //Move to ground position
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            RaycastHit hit;
            if (Physics.Raycast(cam.ScreenPointToRay(mousePosition), out hit, 1000000))
            {
                agent.isStopped = false;
                agent.destination = hit.point;
                
                onGroundPosPressed.Invoke();
            }
        }
        
        //Stop in melee range
        if (movingToSelected && Vector3.Distance(transform.position, selectedPosition) <= distance.meleeRadius)
        {
            agent.isStopped = true;
            
            onMeleePosReached.Invoke();
            
            movingToSelected = false;
            selectedPosition = new Vector3();
        }
    }

    public void MeleeMoveToSelected(Vector3 _selectedPosition)
    {
        agent.SetDestination(_selectedPosition);
        selectedPosition = _selectedPosition;
        movingToSelected = true;
    }
}

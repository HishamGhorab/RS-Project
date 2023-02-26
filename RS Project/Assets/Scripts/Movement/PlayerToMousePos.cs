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

        /*if (Approximation(GetTotalVel(), 0, 0.05f))
        {
            Debug.Log("hi");
            agent.velocity = Vector3.zero;
            agent.ResetPath();
        }*/

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

    public float GetTotalVel()
    {
        return agent.velocity.x + agent.velocity.y + agent.velocity.z;
    }
    
    /// <summary>
    /// compare both values (a, b) and see if the difference is between threshold
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="threshold"></param>
    /// <returns></returns>
    public static bool Approximation(float a, float b, float threshold)
    {
        return (Mathf.Abs(a - b) < threshold); // compare both values and see if the difference is between threshold
    }
}

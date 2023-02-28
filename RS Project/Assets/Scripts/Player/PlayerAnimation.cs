using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimation : MonoBehaviour
{
    public Player owner;
    
    private void OnEnable()
    {
        Interact.OnGatheringEventStart += StartGatherAnim;
        Interact.OnGatheringEventEnd += StopGatherAnim;
    }

    private void OnDisable()
    {
        Interact.OnGatheringEventStart -= StartGatherAnim;
        Interact.OnGatheringEventEnd -= StopGatherAnim;
    }

    private void Update()
    {
        Debug.Log(owner);
        
        if (owner.AgentComponent.velocity != Vector3.zero)
        {
            //Debug.Log("moving");
            owner.AnimatorComponent.SetBool("PlayerWalk", true);
        }
        else
        {
            //Debug.Log("not moving");
            owner.AnimatorComponent.SetBool("PlayerWalk", false);
        }
    }

    public void StartGatherAnim()
    {
        owner.AnimatorComponent.SetBool("PlayerGather", true);
    }
    
    public void StopGatherAnim()
    {
        owner.AnimatorComponent.SetBool("PlayerGather", false);
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimation : MonoBehaviour
{
    public NavMeshAgent agent;
    private Animator animationController;

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

    private void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
        animationController = GetComponent<Animator>();
    }

    private void Update()
    {
        //Debug.Log(agent.velocity);
        
        if (agent.velocity != Vector3.zero)
        {
            //Debug.Log("moving");
            animationController.SetBool("PlayerWalk", true);
        }
        else
        {
            //Debug.Log("not moving");
            animationController.SetBool("PlayerWalk", false);
        }
    }

    public void StartGatherAnim()
    {
        animationController.SetBool("PlayerGather", true);
    }
    
    public void StopGatherAnim()
    {
        animationController.SetBool("PlayerGather", false);
    }
}


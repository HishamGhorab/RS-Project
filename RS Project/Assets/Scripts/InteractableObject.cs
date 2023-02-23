using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class InteractableObject : MonoBehaviour
{
    public enum InteractionType {Gathering, Monster};
    public InteractionType interactionType;
    
    private Outline outline;

    void Start()
    {
        outline = GetComponent<Outline>();
        OnDeselected();
    }

    public void OnHighlighted()
    {
        outline.enabled = true;
    }
    
    public void OnSelected()
    {
        outline.enabled = true;
    }
    
    public void OnDeselected()
    {
        outline.enabled = false;
    }
}
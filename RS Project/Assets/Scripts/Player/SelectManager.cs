using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    //if mouse hovers object with tag or script
    //Highlight it
    
    //if highlighted and pressed button
    //select
    
    [NonSerialized] public InteractableObject highlightedObject;
    private bool pressed;

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Camera cam = Camera.main;
        
        RaycastHit hit;
        if (Physics.Raycast(cam.ScreenPointToRay(mousePosition), out hit, 1000000))
        {
            if (!hit.collider.GetComponent<InteractableObject>())
            {
                //not pressed but previously highlighted
                if (!pressed && highlightedObject)
                {
                    highlightedObject.OnDeselected();
                    highlightedObject = null;
                }
                
                //previously pressed but not pressing ground
                if (Input.GetMouseButtonDown(0) && pressed)
                {
                    if (highlightedObject)
                    {
                        highlightedObject.OnDeselected();
                        highlightedObject = null;
                    }
                    pressed = false;
                }
                
                return;
            }

            InteractableObject interactableObj = hit.collider.GetComponent<InteractableObject>();
            interactableObj.OnHighlighted();
            
            //pressed
            if (Input.GetMouseButtonDown(0) && hit.collider.GetComponent<InteractableObject>())
            {
                pressed = true;
                GetComponent<PlayerToMousePos>().MeleeMoveToSelected(hit.collider.transform.position);
            }

            highlightedObject = interactableObj;
        }
    }
}

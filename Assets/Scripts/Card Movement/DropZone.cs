//author: Lex schenning
//date: 14/12/2023
//description: This script is used to drop and snap the cards
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Draggable.Slot typeOfSide = Draggable.Slot.FRIENDLY;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Debug.Log("OnPointerEnter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Debug.Log("OnPointerExit");
    }

    public void OnDrop(PointerEventData eventData)
    {
        // Log a message to the Unity console indicating that a draggable object was dropped onto the current object.
        

        // Get the Draggable component of the dragged object.
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

        // Check if the Draggable component is not null (i.e., the dragged object has the Draggable component).
        if (d != null)
        {
            // If the target object already has a child, return the dragged object to its original parent.
            if (this.transform.childCount > 0)
            {
                Transform originalParent = d.parentToReturnTo;
                d.parentToReturnTo = originalParent;
            }
            else
            {
                // If the target object doesn't have a child, set the parentToReturnTo property to the current object's transform.
                if (typeOfSide == d.typeOfSide)
                {
                    d.parentToReturnTo = this.transform;
                }
            }
        }
    }
}

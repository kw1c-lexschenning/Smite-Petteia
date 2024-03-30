//author: Lex schenning
//date: 14/12/2023
//description: This script is used to move the cards
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public enum Slot { FRIENDLY, ENEMY };
    public Slot typeOfSide = Slot.FRIENDLY;
    public Transform parentToReturnTo = null;
    

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Store the current parent of the card and move it to a higher level in the hierarchy
        parentToReturnTo = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);

        // Disable raycasting for the dragged card to allow interaction with objects behind it
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Update the card's position to follow the mouse/finger position during the drag
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Return the card to its original parent and re-enable raycasting
        this.transform.SetParent(parentToReturnTo);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}

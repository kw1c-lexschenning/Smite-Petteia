// Author: Lex Schenning
// Date: 26-03-2024
// Version: 1.0
// Description: This script represents a tab button in the game UI. It handles pointer events such as click, enter, and exit, and notifies the associated tab group.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Ensure that the GameObject has an Image component
[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    // Reference to the tab group this button belongs to
    public TabGroup tabGroup;
    // Reference to the background image of the button
    public Image background;

    // Method called when the button is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        // Notify the tab group that this tab button is selected
        tabGroup.OnTabSelected(this);
    }

    // Method called when the pointer enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Notify the tab group that the pointer entered this tab button
        tabGroup.OnTabEnter(this);
    }

    // Method called when the pointer exits the button
    public void OnPointerExit(PointerEventData eventData)
    {
        // Notify the tab group that the pointer exited this tab button
        tabGroup.OnTabExit(this);
    }

    // Method called on start
    public void Start()
    {
        // Get the Image component attached to this GameObject
        background = GetComponent<Image>();
        // Subscribe this tab button to the tab group
        tabGroup.Subscribe(this);
    }
}

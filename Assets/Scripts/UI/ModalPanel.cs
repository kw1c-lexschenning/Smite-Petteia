// Author: Lex Schenning
// Date: 26-03-2024
// Version: 1.0
// Description: This script manages a modal panel in the game. It provides functionality to handle tab interactions such as mouse hover, click, and setting button colors.
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ModalPanel : MonoBehaviour
{
    // Reference to the confirm button
    public Transform confirm;
    // Reference to the cancel button
    public Transform cancel;
    // Reference to the tab controller
    public Transform tabController;

    // Reference to the outline component
    public Outline outline;
    // Color when tab is idle
    public Color tabIdle;
    // Color when tab is hovered over
    public Color tabHover;

    // Method called on start
    public void Start()
    {
        // Get the outline component
        outline = GetComponent<Outline>();
        // Set the outline effect color to idle color
        outline.effectColor = tabIdle;
    }

    // Method called when mouse enters the tab
    public void OTabEnter(PointerEventData eventData)
    {
        // Change outline effect color to hover color
        outline.effectColor = tabHover;
    }

    // Method called when mouse exits the tab
    public void OnTabExit(PointerEventData eventData)
    {
        // Change outline effect color back to idle color
        outline.effectColor = tabIdle;
    }

    // Method called when the tab is clicked
    public void OTabClick(PointerEventData eventData)
    {
        // Check if the clicked object is the confirm button
        if (gameObject == confirm)
        {
            // Quit the application
            Application.Quit();
        }
        // Check if the clicked object is the cancel button
        if (gameObject == cancel)
        {
            // Disable the modal panel
            gameObject.SetActive(false);
        }
    }
}

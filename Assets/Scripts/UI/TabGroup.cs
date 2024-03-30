// Author: Lex Schenning
// Date: 26-03-2024
// Version: 1.0
// Description: This script manages a group of tab buttons in the game UI. It handles tab selection, hover effects, and swapping associated panels.
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    // List of tab buttons in the tab group
    public List<TabButton> tabButtons;
    // Color when tab is idle
    public Color tabIdle;
    // Color when tab is hovered over
    public Color tabHover;
    // Color when tab is active/selected
    public Color tabActive;
    // Reference to the currently selected tab  
    public TabButton selectedTab;
    // List of objects to swap based on tab selection
    public List<GameObject> objectsToSwap;
    // Reference to the panel group associated with the tabs
    public PanelGroup panelGroup;

    // Method to subscribe a tab button to the tab group
    public void Subscribe(TabButton button)
    {
        // Initialize the list if it's null
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }
        // Add the button to the list
        tabButtons.Add(button);
    }

    // Method called when the pointer enters a tab button
    public void OnTabEnter(TabButton button)
    {
        // Reset all tabs' colors
        ResetTabs();
        // Change the button's color if it's not already selected
        if (selectedTab == null || button != selectedTab)
        {
            button.background.color = tabHover;
        }
    }

    // Method called when the pointer exits a tab button
    public void OnTabExit(TabButton button)
    {
        // Reset all tabs' colors
        ResetTabs();
    }

    // Method called when a tab button is selected
    public void OnTabSelected(TabButton button)
    {
        // Set the current panel index based on the selected button
        panelGroup.SetPageIndex(tabButtons.IndexOf(button));
        // Set the selected tab
        selectedTab = button;
        // Reset all tabs' colors
        ResetTabs();
        // Set the selected tab's color to active
        button.background.color = tabActive;
        // Get the index of the selected button in the hierarchy
        int index = button.transform.GetSiblingIndex();
        // Toggle the visibility of associated objects based on the selected tab
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    // Method to reset all tab colors to idle
    public void ResetTabs()
    {
        foreach (TabButton button in tabButtons)
        {
            // Skip the selected tab
            if (selectedTab != null && button == selectedTab) { continue; }
            // Reset tab color to idle
            button.background.color = tabIdle;
        }
    }
}

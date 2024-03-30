// Author: Lex Schenning
// Date: 26-03-2024
// Version: 1.0
// Description: This script manages a group of panels in the game. It provides functionality to show/hide panels based on the selected tab and set the current panel index.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelGroup : MonoBehaviour
{
    // Array of panels to manage
    public GameObject[] panels;

    // Reference to the tab group associated with the panels
    public TabGroup tabGroup;

    // Index of the current panel
    public int panelIndex;

    // Method called before the first frame update
    void Awake()
    {
        // Show the current panel upon awake
        ShowCurrentPanel();
    }

    // Method to show the current panel and hide others
    void ShowCurrentPanel()
    {
        // Iterate through all panels
        for (int i = 0; i < panels.Length; i++)
        {
            // Activate the panel if its index matches the current panel index
            if (i == panelIndex)
            {
                panels[i].gameObject.SetActive(true);
            }
            // Deactivate the panel if its index does not match the current panel index
            else
            {
                panels[i].gameObject.SetActive(false);
            }
        }
    }

    // Method to set the current panel index
    public void SetPageIndex(int index)
    {
        // Set the panel index
        panelIndex = index;
        // Show the current panel
        ShowCurrentPanel();
    }
}

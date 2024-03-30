// Author: Lex Schenning
// Date: 26-03-2024
// Version: 1.0
// Description: This script manages the main menu of the game. It provides functionality to start the game, quit the application, and change settings such as resolution and fullscreen mode.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Dropdown UI element to select resolutions
    public TMP_Dropdown resolutionDropdown;

    // Array to store available resolutions
    Resolution[] resolutions;

    void Start()
    {
        // Set the game to fullscreen mode
        SetFullScreen(true);

        // Retrieve available screen resolutions
        resolutions = Screen.resolutions;

        // Clear options in the resolution dropdown
        resolutionDropdown.ClearOptions();

        // List to hold resolution options
        List<string> options = new List<string>();

        // Variable to store the index of the current resolution
        int currentResolutionIndex = 0;

        // Iterate through available resolutions
        for (int i = 0; i < resolutions.Length; i++)
        {
            // Construct resolution option string
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // Check if current resolution matches the screen's current resolution
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                // Set the index to the current resolution
                currentResolutionIndex = i;
            }
        }

        // Add resolution options to the dropdown
        resolutionDropdown.AddOptions(options);
        // Set the value of the dropdown to the current resolution index
        resolutionDropdown.value = currentResolutionIndex;
        // Refresh the displayed value of the dropdown
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.AddOptions(options);
    }

    // Method to set the game resolution
    public void SetResolution(int resolutionIndex)
    {
        // Retrieve the selected resolution from the array
        Resolution resolution = resolutions[resolutionIndex];
        // Set the game resolution
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // Method to toggle fullscreen mode
    public void SetFullScreen(bool isFullscreen)
    {
        // Set fullscreen mode based on input
        Screen.fullScreen = isFullscreen;
    }

    // Method to quit the application
    public void Quitgame()
    {
        // Quit the application
        Application.Quit();
    }

    // Method to start the game
    public void Playgame()
    {
        // Load the game scene asynchronously
        SceneManager.LoadSceneAsync((int)SceneIndexes.SceneIndex.Game);
    }
}

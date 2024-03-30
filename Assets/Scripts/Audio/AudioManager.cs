// Author: Lex Schenning
// Date: 26-03-2024
// Version: 1.0
// Description: This script manages audio volumes using an AudioMixer.
// It provides methods to adjust the main volume, SFX volume, and music volume.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // Reference to the AudioMixer
    public AudioMixer AudioMixer;

    // Sets the main volume level
    public void SetMainVolume(float volume)
    {
        // Convert volume to decibel scale and apply to the main volume parameter
        AudioMixer.SetFloat("MainVolume", Mathf.Log10(volume) * 20f);
    }

    // Sets the SFX volume level
    public void SetSFXVolume(float volume)
    {
        // Convert volume to decibel scale and apply to the SFX volume parameter
        AudioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20f);
    }

    // Sets the music volume level
    public void SetMusicVolume(float volume)
    {
        // Convert volume to decibel scale and apply to the music volume parameter
        AudioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20f);
    }
}

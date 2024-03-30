//Author: Lex Schenning
//Date: 26-03-2024
//Version: 1.0
//Description: This script manages audio levels for different categories of sound effects (SFX) in the game.
//It provides methods to adjust the volume levels of the main audio, music, SFX, and announcer.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSFXManager : MonoBehaviour
{
    // Reference to the AudioMixer to control audio levels
    public AudioMixer audioMixer;

    // Sets the main volume level
    public void SetMainVolume(float volume)
    {
        audioMixer.SetFloat("MainVolume", volume);
    }

    // Sets the music volume level
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    // Sets the SFX volume level
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }

    // Sets the announcer volume level
    public void SetAnnouncerVolume(float volume)
    {
        audioMixer.SetFloat("AnnouncerVolume", volume);
    }
}

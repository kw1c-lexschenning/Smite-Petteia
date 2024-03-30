// Author: Lex Schenning
// Date: 26-03-2024
// Version: 1.0
// Description: This script manages playing sound effects (SFX) in the game.
// It allows for playing SFX at specified locations with custom volumes.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    // Singleton instance of the SFXManager
    public static SFXManager instance;

    // Reference to the SFX audio source prefab
    [SerializeField] private AudioSource sfxObject;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Ensure only one instance of SFXManager exists
        if (instance == null)
        {
            instance = this;
        }
    }

    // Plays a sound effect at the specified location with the given volume
    public void PlaySFX(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        // Instantiate a new audio source for the SFX
        AudioSource audioSource = Instantiate(sfxObject, spawnTransform.position, Quaternion.identity);

        // Set the audio clip to be played
        audioSource.clip = audioClip;

        // Set the volume of the audio source
        audioSource.volume = volume;

        // Play the SFX
        audioSource.Play();

        // Calculate the length of the audio clip
        float clipLength = audioSource.clip.length;

        // Destroy the audio source object after the clip has finished playing
        Destroy(audioSource.gameObject, clipLength);
    }
}
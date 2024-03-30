// SFXTrigger.cs
// Author: Lex Schenning
// Date: 26-03-2024
// Version: 1.0
// Description: This script handles triggering sound effects (SFX) in response to pointer clicks.
// It implements the IPointerClickHandler interface to detect click events on the object it's attached to.
// When clicked, it plays a specified sound clip using the SFXManager, passing the transform of the object as a reference and setting the volume to 1.
using UnityEngine.EventSystems;
using UnityEngine;

public class SFXTrigger : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private AudioClip tabSoundClip; // Sound clip to be played on click
    private SFXManager sfxManager; // Reference to the SFXManager instance

    // Called when the object is initialized
    private void Start()
    {
        // Find and assign the SFXManager instance
        sfxManager = FindObjectOfType<SFXManager>();
    }

    // Called when a pointer (e.g., mouse) clicks on the object
    public void OnPointerClick(PointerEventData eventData)
    {
        // Play the specified sound clip with SFXManager
        // Parameters: sound clip, transform of this object, volume
        sfxManager.PlaySFX(tabSoundClip, transform, 1f);
    }
}
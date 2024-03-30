// Author: Lex Schenning
// Date: 26-03-2024
// Version: 1.0
// Description: This script creates a hover animation for cards in the game.
// It moves the card cover and stats upward and scales the card when the mouse hovers over it.
// Upon mouse exit, it returns the cover and stats to their original positions and scales the card back.
// Animation speed and hover offsets can be adjusted in the inspector.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardHoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // References to card cover and stats transforms
    public Transform Cover;
    public Transform Stats;

    // Original positions and scale of the card
    private Vector3 CoverOriginalPosition;
    private Vector3 StatsOriginalPosition;
    private float CardOriginalScale;

    // Adjustable hover offsets and animation speed


    public float CoverhoverOffset; // Adjust this value to change the hover offset
    public float StatshoverOffset; // Adjust this value to change the hover offset
    public float CardhoverOffset; // Adjust this value to change the hover scale
    public float AnimationSpeed; // Adjust this value to change the animation speed

    // Start is called before the first frame update
    public void Start()
    {
        // Store original positions and scale
        CoverOriginalPosition = Cover.localPosition;
        StatsOriginalPosition = Stats.localPosition;
        CardOriginalScale = this.transform.localScale.x;
    }

    // Called when the mouse enters the card area
    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector3 covertargetPosition = CoverOriginalPosition + new Vector3(0, CoverhoverOffset, 0);
        LeanTween.moveLocalY(Cover.gameObject, covertargetPosition.y, AnimationSpeed).setEaseOutQuint();

        Vector3 stattargetPosition = StatsOriginalPosition + new Vector3(0, StatshoverOffset, 0);
        LeanTween.moveLocalY(Stats.gameObject, stattargetPosition.y, AnimationSpeed).setEaseOutQuint();

        LeanTween.scale(this.gameObject, new Vector3(CardhoverOffset, CardhoverOffset, CardhoverOffset), AnimationSpeed).setEaseOutQuint();
    }

    // Called when the mouse exits the card area
    public void OnPointerExit(PointerEventData eventData)
    {
        // Return elements to their original positions and scale
        LeanTween.moveLocalY(Cover.gameObject, CoverOriginalPosition.y, AnimationSpeed).setEaseOutQuint();
        LeanTween.moveLocalY(Stats.gameObject, StatsOriginalPosition.y, AnimationSpeed).setEaseOutQuint();
        LeanTween.scale(this.gameObject, new Vector3(CardOriginalScale, CardOriginalScale, CardOriginalScale), AnimationSpeed).setEaseOutQuint();
    }
}

//Card.cs
//Author: Lex Schenning
//Date: 33/11/2023
//Description: This script is used to store the card data.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allows creating new Card assets via Unity's CreateAssetMenu
[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    // Reference to the GameObject representing the card in the game world
    public GameObject cardObject;

    // Unique identifier for the card
    public int id;

    // Name of the card
    public new string name;

    // Description of the card
    public string description;

    // Artwork for the card
    public Sprite artwork;

    // Power cost required to play the card
    public int PowerCost;

    // Attack value of the card
    public int attack;

    // Health value of the card
    public int health;
}

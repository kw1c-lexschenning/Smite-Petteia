//author: Lex schenning
//date: 22/11/2023
//description: This script is used to display the card data on the board
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;
    public TMP_Text nameText;
    public TMP_Text descriptionText;

    public Image artworkImage;

    public TMP_Text attackText;
    public TMP_Text healthText;
    public TMP_Text powerText;


    // Start is called before the first frame update
    public void Start()
    {
        nameText.text = card.name;
        descriptionText.text = card.description;
        artworkImage.sprite = card.artwork;
        attackText.text = card.attack.ToString();
        healthText.text = card.health.ToString();
        powerText.text = card.PowerCost.ToString();
    }
}

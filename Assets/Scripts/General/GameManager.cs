// Author: Lex Schenning
// Date: 17/01/2023
// Description: This script manages the game logic.

// Importing necessary libraries
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Declaring variables
    public List<Card> deck = new List<Card>(); // List of cards in the deck
    private List<int> usedIndices = new List<int>(); // Indices of used cards
    public GameObject cardPrefab; // Prefab for card game object
    public Transform[] FriendlyCardSlots; // Slots for friendly cards
    public Transform[] EnemyCardSlots; // Slots for enemy cards
    public Transform[] SpawnCardSlots; // Slots for spawning friendly cards
    public Transform[] EnemySpawnCardSlots; // Slots for spawning enemy cards
    public int i; // Index variable
    public int a; // Index variable
    public int EnemyPlayerHealth; // Health of enemy player
    public int FriendlyPlayerHealth; // Health of friendly player
    public int round; // Current round number
    public int Friendlyhealth; // Health of friendly cards
    public int Friendlyattack; // Attack power of friendly cards
    public int Enemyhealth; // Health of enemy cards
    public int Enemyattack; // Attack power of enemy cards
    public int Boardmana; // Current mana on board
    public int EnemyPower; // Power of enemy cards
    public int TotalEnemyPower; // Total power of enemy cards
    public int FriendlyPower; // Power of friendly cards
    public int TotalFriendlyPower; // Total power of friendly cards
    public int MaxPower; // Maximum power allowed
    public TMP_Text PlayerHPText; // Text for displaying friendly player health
    public TMP_Text EnemyHPText; // Text for displaying enemy player health
    public TMP_Text RoundCounter; // Text for displaying current round number
    public TMP_Text PowerCounter; // Text for displaying total power
    public TMP_Text PowerSpentCounter; // Text for displaying power spent
    public TMP_Text Message; // Text for displaying messages
    public Button CardRefresh; // Button for refreshing cards
    public Button EndTurn; // Button for ending turn
    public CardDisplay cardDisplayPrefab; // Prefab for card display

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        // Update the power of friendly board
        GetFriendlyboardPower();
    }

    // Return to the main menu
    public void ReturnToMenu()
    {
        SceneManager.LoadSceneAsync((int)SceneIndexes.SceneIndex.MainMenu);
    }

    // Start a new round
    public void Roundstart()
    {
        // Update player display
        UpdatePlayerDisplay();
        round++;
        RoundCounter.text = "" + round;
        CardRefresh.interactable = true;
        
        // Draw enemy cards
        DrawEnemyCards();
        // Calculate enemy board power and execute enemy player algorithm
        GetEnemyboardPower();
        EnemyPlayerAlgorithm();   
    }

    // Calculate the total power of friendly board
    public void GetFriendlyboardPower()
    {
        TotalFriendlyPower = 0;
        
        for (int i = 0; i < FriendlyCardSlots.Length; i++)
        {
            
            if (FriendlyCardSlots[i].childCount > 0)
            {
                Transform cardTransform = FriendlyCardSlots[i].GetChild(0);
                CardDisplay cardDisplay = cardTransform.GetComponent<CardDisplay>();

                 FriendlyPower = int.Parse(cardDisplay.powerText.text);
                 TotalFriendlyPower = TotalFriendlyPower + FriendlyPower;
            }

            if (TotalFriendlyPower > MaxPower)
            {
                Message.text = "Insufficient Power";
                EndTurn.interactable = false;
            }
            else
            {
                EndTurn.interactable = true;
                Message.text = "";
            }
            
            PowerSpentCounter.text = "" + TotalFriendlyPower;
            PowerCounter.text = "" + MaxPower;
        }

        TotalFriendlyPower = 0;
    }

    // Calculate the total power of enemy board
    public void GetEnemyboardPower()
    {
        TotalEnemyPower = 0;
        EnemyPower = 0;

        for (int i = 0; i < EnemyCardSlots.Length; i++)
        {
            if (EnemyCardSlots[i].childCount > 0)
            {
                Transform cardTransform = EnemyCardSlots[i].GetChild(0);
                CardDisplay cardDisplay = cardTransform.GetComponent<CardDisplay>();

                EnemyPower = int.Parse(cardDisplay.powerText.text);
                TotalEnemyPower = TotalEnemyPower + EnemyPower;
                if (usedIndices.Count == EnemySpawnCardSlots.Length)
                {
                    // Reset used indices list
                    usedIndices.Clear();
                }
                usedIndices.Add(i);
            }
        }
    }

    // Execute enemy player algorithm
    public void EnemyPlayerAlgorithm()
    {
        for (int i = 0; i < EnemySpawnCardSlots.Length; i++)
        {
            if (TotalEnemyPower < MaxPower)
            {
                // Check if all card slots have been used
                int randomIndex;
                Transform cardTransform;
                do
                {
                    // Generate a random index until an unused index is found
                    randomIndex = Random.Range(0, EnemySpawnCardSlots.Length);

                } while (usedIndices.Contains(randomIndex));

                // Add the index to the list of used indices
                usedIndices.Add(randomIndex);

                // Get the card transform from the selected slot
                cardTransform = EnemySpawnCardSlots[randomIndex].GetChild(0);

                CardDisplay cardDisplay = cardTransform.GetComponent<CardDisplay>();

                EnemyPower = int.Parse(cardDisplay.powerText.text);

                TotalEnemyPower = TotalEnemyPower + EnemyPower;

                if (TotalEnemyPower < MaxPower)
                {
                    EnemySpawnCardSlots[randomIndex].GetChild(0).SetParent(EnemyCardSlots[randomIndex], false);

                }
                else
                {
                    TotalEnemyPower = TotalEnemyPower - EnemyPower;

                    if (usedIndices.Count > EnemySpawnCardSlots.Length)
                    {
                        break;
                    }
                }

            }
        }
    }

    // Draw cards for the enemy
    public void DrawEnemyCards()
    {
        for (int i = 0; i < EnemySpawnCardSlots.Length; i++)
        {
            if (EnemySpawnCardSlots[i].childCount < 1)
            {
                // Spawn a random card
                SpawnRandomEnemyCard(i); // Pass the correct slot index
            }
        }
    }

    // Draw cards for the friendly player
    public void DrawFriendlyCards()
    {
        for (int i = 0; i < SpawnCardSlots.Length; i++)
        {
            if (SpawnCardSlots[i].childCount < 1)
            {
                // Spawn a random card
                SpawnRandomFriendlyCard(i); // Pass the correct slot index
            }
        }
    }

    // Spawn a random card for the enemy
    private void SpawnRandomEnemyCard(int slotIndex)
    {
        // Select a random card from the deck
        Card randCard = deck[UnityEngine.Random.Range(0, deck.Count)];

        // Instantiate a new card object using the cardPrefab and assign it to a card slot
        GameObject newCardObject = Instantiate(cardPrefab, EnemySpawnCardSlots[slotIndex]);

        newCardObject.GetComponent<Draggable>().enabled = false;

        // Get the CardDisplay component from the new card object
        CardDisplay cardDisplay = newCardObject.GetComponent<CardDisplay>();

        // Check if the CardDisplay component is not null
        if (cardDisplay != null)
        {
            // Assign the random card to the CardDisplay's card property
            cardDisplay.card = randCard;

            // Call the Start method on the CardDisplay (assuming it has a Start method)
            cardDisplay.Start();
        }
        // Remove draggable component
    }

    // Spawn a random card for the friendly player
    private void SpawnRandomFriendlyCard(int slotIndex)
    {
        // Select a random card from the deck
        Card randCard = deck[UnityEngine.Random.Range(0, deck.Count)];

        // Instantiate a new card object using the cardPrefab and assign it to a card slot
        GameObject newCardObject = Instantiate(cardPrefab, SpawnCardSlots[slotIndex]);

        // Get the CardDisplay component from the new card object
        CardDisplay cardDisplay = newCardObject.GetComponent<CardDisplay>();

        // Check if the CardDisplay component is not null
        if (cardDisplay != null)
        {
            // Assign the random card to the CardDisplay's card property
            cardDisplay.card = randCard;

            // Call the Start method on the CardDisplay (assuming it has a Start method)
            cardDisplay.Start();
        }
        CardRefresh.interactable = false;
    }

    // Get data of cards on the board
    public void GetCardData()
    {
        // Assuming that the enemy and friendly card slots are always the same length
        for (int a = 0; a < EnemyCardSlots.Length;  a++) 
        {
             Friendlyhealth = 0;
             Friendlyattack = 0;
             Enemyhealth = 0;
             Enemyattack = 0;

            if (FriendlyCardSlots[a].childCount > 0)
            {
                Transform cardTransform = FriendlyCardSlots[a].GetChild(0);
                CardDisplay cardDisplay = cardTransform.GetComponent<CardDisplay>();

                Friendlyattack = int.Parse(cardDisplay.attackText.text);
                Friendlyhealth = int.Parse(cardDisplay.healthText.text);   
            }
            if (EnemyCardSlots[a].childCount > 0)
            {
                Transform cardTransform = EnemyCardSlots[a].GetChild(0);
                CardDisplay cardDisplay = cardTransform.GetComponent<CardDisplay>();

                Enemyattack = int.Parse(cardDisplay.attackText.text);
                Enemyhealth = int.Parse(cardDisplay.healthText.text);
            }
            // Execute card fight
            CardFight(a);
        }
        // Start a new round
        Roundstart();
    }

    // Simulate a fight between cards
    public void CardFight(int a)
    {
        Enemyhealth -= Friendlyattack;
        Friendlyhealth -= Enemyattack;

        if (FriendlyCardSlots[a].childCount > 0 && EnemyCardSlots[a].childCount > 0)
        {
            if (Friendlyhealth <= 0)
            {
                Transform childTransform = FriendlyCardSlots[a].GetChild(0);
                if (childTransform != null)
                {
                    Friendlyhealth = 0;
                    Destroy(childTransform.gameObject);
                }
            }
            if (Enemyhealth <= 0)
            {
                Transform childTransform = EnemyCardSlots[a].GetChild(0);
                if (childTransform != null)
                {
                    Enemyhealth = 0;
                    Destroy(childTransform.gameObject);
                }
            }
            if (Enemyhealth < 0)
            {
                DamageEnemyplayer();
            }
            if (Friendlyhealth < 0)
            {
                DamageFriendlyplayer();
            }
            UpdateCardDisplay(a);
        }
    }

    // Deal damage to the friendly player
    public void DamageFriendlyplayer()
    {        
        // here Friendlyhealth is a - value
        FriendlyPlayerHealth += Friendlyhealth;
    }

    // Deal damage to the enemy player
    public void DamageEnemyplayer()
    {
        // here Friendlyhealth is a - value
        EnemyPlayerHealth += Enemyhealth;
    }

    // Update card display after a fight
    public void UpdateCardDisplay(int a)
    {
        Transform cardTransformEnemy = EnemyCardSlots[a].GetChild(0);
        CardDisplay cardDisplayEnemy = cardTransformEnemy.GetComponent<CardDisplay>();

        cardDisplayEnemy.healthText.text = Enemyhealth.ToString();

        Transform cardTransformFriendly = FriendlyCardSlots[a].GetChild(0);
        CardDisplay cardDisplayFriendly = cardTransformFriendly.GetComponent<CardDisplay>();

        cardDisplayFriendly.healthText.text = Friendlyhealth.ToString();
    }

    // Update player display after a fight
    public void UpdatePlayerDisplay()
    {
        PlayerHPText.text = FriendlyPlayerHealth.ToString();
        EnemyHPText.text = EnemyPlayerHealth.ToString();
    }
}

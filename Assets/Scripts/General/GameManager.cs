//author: Lex schenning
//date: 17/01/2023
//description: This script is used to manage the game
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
    public List<Card> deck = new List<Card>();
    private List<int> usedIndices = new List<int>();
    public GameObject cardPrefab;
    public Transform[] FriendlyCardSlots;
    public Transform[] EnemyCardSlots;
    public Transform[] SpawnCardSlots;
    public Transform[] EnemySpawnCardSlots;
    public int i;
    public int a;
    public int EnemyPlayerHealth;
    public int FriendlyPlayerHealth;
    public int round;
    public int Friendlyhealth;
    public int Friendlyattack;
    public int Enemyhealth;
    public int Enemyattack;
    public int Boardmana;
    public int EnemyPower;
    public int TotalEnemyPower;
    public int FriendlyPower;
    public int TotalFriendlyPower;
    public int MaxPower;
    public TMP_Text PlayerHPText;
    public TMP_Text EnemyHPText;
    public TMP_Text RoundCounter;
    public TMP_Text PowerCounter;
    public TMP_Text PowerSpentCounter;
    public TMP_Text Message;
    public Button CardRefresh;
    public Button EndTurn;
    public CardDisplay cardDisplayPrefab;


    private void Awake()
    {

    }
    void Start()
    {


    }
    void Update()
    {
        
        GetFriendlyboardPower();

    }

    public void ReturnToMenu() 
    {
        SceneManager.LoadSceneAsync((int)SceneIndexes.SceneIndex.MainMenu);
    }

    public void Roundstart()
    {
        UpdatePlayerDisplay();
        round++;
        RoundCounter.text = "" + round;
        CardRefresh.interactable = true;
        
        DrawEnemyCards();
        GetEnemyboardPower();
        EnemyPlayerAlgorithm();   
    }

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
                

                Message.text = "insufficient Power";

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

        //remove draggabe compnent
        
        
    }

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

    public void GetCardData()
    {

        //shit be crashing 
        // asuming that the enemy and friendly cards slots are always the same length
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
            
            CardFight(a);


        }
        
        Roundstart();
        
    }

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

    public void DamageFriendlyplayer()
    {        
        FriendlyPlayerHealth += Friendlyhealth;
    }

    public void DamageEnemyplayer()
    {
        EnemyPlayerHealth += Enemyhealth;
    }

    public void UpdateCardDisplay(int a)
    {
       

        Transform cardTransformEnemy = EnemyCardSlots[a].GetChild(0);
        CardDisplay cardDisplayEnemy = cardTransformEnemy.GetComponent<CardDisplay>();


        cardDisplayEnemy.healthText.text = Enemyhealth.ToString();


        

        Transform cardTransformFriendly = FriendlyCardSlots[a].GetChild(0);
        CardDisplay cardDisplayFriendly = cardTransformFriendly.GetComponent<CardDisplay>();



        cardDisplayFriendly.healthText.text = Friendlyhealth.ToString();
    }

    public void UpdatePlayerDisplay()
    {
        PlayerHPText.text = FriendlyPlayerHealth.ToString();
        EnemyHPText.text = EnemyPlayerHealth.ToString();
    }
}

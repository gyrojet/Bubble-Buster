using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;
using System.Collections;

public class CardDeck : MonoBehaviour
{
    [SerializeField] private List<Card> deckOfCards;

    [SerializeField] private List<Card> backupDeck;

    [SerializeField] private int maxHandSize = 11;

    [SerializeField] Player player;                                                         // The player and the Clambler
    [SerializeField] Player dealer;


    private void Start()
    {
        CreateCopyOfDeck();
        
    }

    public void DealFirstHand()
    {


        DealCard(player);       // Deals two cards to player, one card to dealer
        DealCard(player);

        DealCard(dealer);
    }

    public void DealCard(Player recipiant)
    {
        if (recipiant.PlayersHandSize >= maxHandSize)
        {
            Debug.Log("MAXIMUM OF 5 CARDS IN HAND");
        } 
        else
        {
            int rand = Random.Range(0, deckOfCards.Count);

            Card dealtCard = deckOfCards[rand];

            recipiant.HitMe(dealtCard);

            Debug.Log($"Dealing {dealtCard.CardRankType} of {dealtCard.CardSuit}s to {recipiant}");

            deckOfCards.Remove(dealtCard);

            recipiant.SetCardFaces();
        }
        
    }

    private void CreateCopyOfDeck()
    {
        backupDeck = new List<Card>(deckOfCards);
    }

    public void RefreshDeck()
    {
        Debug.Log("DeckRefresh");
        deckOfCards = new List<Card>(backupDeck);
    }
}

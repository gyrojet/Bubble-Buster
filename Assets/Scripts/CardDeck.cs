using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class CardDeck : MonoBehaviour
{
    [SerializeField] private List<Card> deckOfCards;

    [SerializeField] Player player;                                                         // The player and the Clambler
    [SerializeField] Player dealer;


    private void Start()
    {
        DealCard(player);
        DealCard(player);

        DealCard(dealer);
        DealCard(dealer);
    }

    public void DealCard(Player recipiant)
    {
        int rand = Random.Range(0, deckOfCards.Count);

        Card dealtCard = deckOfCards[rand];

        recipiant.HitMe(dealtCard);

        Debug.Log($"Dealing {dealtCard.CardRankType} of {dealtCard.CardSuit}s to {recipiant}");

        deckOfCards.Remove(dealtCard);


    }
}

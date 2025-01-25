using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;
using System.Collections;

public class CardDeck : MonoBehaviour
{
    [SerializeField] private List<Card> deckOfCards;

    [SerializeField] private List<Card> copyOfDeck;

    [SerializeField] Player player;                                                         // The player and the Clambler
    [SerializeField] Player dealer;


    private IEnumerator Start()
    {
        CreateCopyOfDeck();

        DealCard(player);

        yield return new WaitForSeconds(1);

        DealCard(player);

        yield return new WaitForSeconds(1);

        DealCard(dealer);

        yield return new WaitForSeconds(1);

        DealCard(dealer);

        yield return new WaitForSeconds(1);

        //player.SetCardFaces();
        //dealer.SetCardFaces();
    }

    public void DealCard(Player recipiant)
    {
        int rand = Random.Range(0, deckOfCards.Count);

        Card dealtCard = deckOfCards[rand];

        recipiant.HitMe(dealtCard);

        Debug.Log($"Dealing {dealtCard.CardRankType} of {dealtCard.CardSuit}s to {recipiant}");

        deckOfCards.Remove(dealtCard);

        recipiant.SetCardFaces();
    }

    private void CreateCopyOfDeck()
    {
        copyOfDeck = deckOfCards;
    }

    public void RefreshDeck()
    {
        deckOfCards = copyOfDeck;
    }
}

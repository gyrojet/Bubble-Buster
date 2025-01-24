using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [SerializeField] private int p_HandValue = 0;                   // A player's hand value
    [SerializeField] private int p_Money;                           // The player's money

    [SerializeField] private List<Card> p_Hand;

    public void HitMe(Card card)
    {
        p_Hand.Add(card);                                           // Add card to hand
        AddScoreToHand(card);                                       // Add card's score to total
    }

    private void AddScoreToHand(Card card)
    {
        if (card.IsAnAce)                                           // If the drawn card is an ace:
        {
            if (p_HandValue + 11 > 21)                              // If ace would bust:
            {
                p_HandValue += 1;                                   // Add one
            }
            else
            {
                p_HandValue += 11;                                  // Else, add 11
            }
        }
        else                                                        // If the card is not an ace:
        {
            p_HandValue += card.Rank;
        }
    }



}
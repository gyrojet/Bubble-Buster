using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public enum Role
    {
        Player,
        Dealer
    }


    [SerializeField] private int p_HandValue = 0;                   // A player's hand value
    [SerializeField] private int p_Money;                           // The player's money

    [SerializeField] private Role p_Role;

    [SerializeField] private List<Card> p_Hand;

    [SerializeField] private List<CardBody> p_CardDisplay;

    [SerializeField] UIManager p_UIManager;

    public int PlayersHandValue { get { return p_HandValue; } }

    public Role PlayerRole { get { return p_Role; } }

    public void HitMe(Card card)
    {
        p_Hand.Add(card);                                           // Add card to hand
        AddScoreToHand(card);                                       // Add card's score to total

        if (p_Role == Role.Player)                                  // Sets score display for Player/Dealer
            p_UIManager.SetScoreDisplay(0);
        else if (p_Role == Role.Dealer)
            p_UIManager.SetScoreDisplay(1);
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

    public void SetCardFaces()
    {
        for (int i = 0; i < p_Hand.Count; i++)
        {
            p_CardDisplay[i].SetFace(p_Hand[i].Face);
        }
    }

    public void ResetHand()
    {
        p_HandValue = 0;

        p_Hand.Clear();
    }



}
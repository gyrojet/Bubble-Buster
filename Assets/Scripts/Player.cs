using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [SerializeField] private int p_HandValue;
    [SerializeField] private int p_Money;

    [SerializeField] private List<Card> p_Hand;

    private void GetHandValues()
    {
        foreach (Card card in p_Hand)
        {
            p_HandValue += card.Rank;
        }
    }



}
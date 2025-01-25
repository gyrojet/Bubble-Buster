using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Player dealer;

    [SerializeField] TextMeshProUGUI ui_PlayerScore;
    [SerializeField] TextMeshProUGUI ui_DealerScore;

    [SerializeField] Button ui_PlayerHit;
    [SerializeField] Button ui_Begin;

    [SerializeField] CardDeck deck;

    public void Start()
    {
        ui_PlayerHit.enabled = false;
        ui_Begin.enabled = true;
    }

    public void SetScoreDisplay(int displayToTarget)
    {
        int handScore;

        switch (displayToTarget) 
        {
            case 0:
                handScore = player.PlayersHandValue;
                ui_PlayerScore.text = handScore.ToString();

                if (handScore > 21)
                    ui_PlayerScore.color = Color.red;
                else
                    ui_PlayerScore.color = Color.white;

                break;
            case 1:
                handScore = dealer.PlayersHandValue;
                ui_DealerScore.text = handScore.ToString();

                if (handScore > 21)
                    ui_DealerScore.color = Color.red;
                else
                    ui_DealerScore.color = Color.white;

                break;
            default:
                Debug.Log("Error in SetScoreDisplay: Invalid switch case!");
                break;


        }
    }

    public void StartRound()
    { 
        ui_Begin.enabled = false;
        ui_PlayerHit.enabled = true;

        player.ResetHand();
        dealer.ResetHand(); 

        deck.RefreshDeck();

        deck.DealFirstHand();
    }

    public void DealPlayerCard()
    {
        deck.DealCard(player);

        if (player.IsPlayerBust)
        {
            
            Debug.Log("Player has gone bust!");

            DealDealerHand();
        }
    }

    private void DealDealerHand()
    {
        //ui_PlayerHit.enabled = false;

        // remove later
        Reset();
    }

    public void Reset()
    {
        ui_Begin.enabled = true;
        ui_PlayerHit.enabled = false;

        player.ResetHand();
        //dealer.ResetHand();

        deck.RefreshDeck();
    }




}

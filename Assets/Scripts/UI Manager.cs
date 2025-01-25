using System.Collections;
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
    [SerializeField] TextMeshProUGUI ui_Results;

    [SerializeField] TextMeshProUGUI ui_PlayerHealth;
    [SerializeField] TextMeshProUGUI ui_DealerHealth;

    [SerializeField] Button ui_PlayerHit;
    [SerializeField] Button ui_PlayerStay;
    [SerializeField] GameObject ui_Begin;

    [SerializeField] GameObject ui_WinScreen;
    [SerializeField] GameObject ui_LoseScreen;

    [SerializeField] CardDeck deck;

    AudioSource audioSource;

    [SerializeField] AudioClip clip_DealCard;
    [SerializeField] AudioClip clip_Lose;
    [SerializeField] AudioClip clip_Win;
    [SerializeField] AudioClip clip_SuperLose;
    [SerializeField] AudioClip clip_SuperWin;

    bool isNewGame = false;

    //[SerializeField] bool playerBust;
    //[SerializeField] bool dealerBust;

    public void Start()
    {
        ui_PlayerHit.interactable = false;
        ui_PlayerStay.interactable = false;
        ui_Begin.SetActive(true);
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

    public void SetHealthDisplay(int displayToTarget)
    {
        int health;

        switch (displayToTarget)
        {
            case 0:
                health = player.PlayerHealth;
                ui_PlayerHealth.text = health.ToString();
                break;
            case 1:
                health = dealer.PlayerHealth;
                ui_DealerHealth.text = health.ToString();

                break;
            default:
                Debug.Log("Error in SetHealthDisplay: Invalid switch case!");
                break;


        }
    }

    public void StartRound()
    { 

        if (isNewGame)
        {
            if (ui_WinScreen.activeSelf == true) { ui_WinScreen.SetActive(false); }
            if (ui_LoseScreen.activeSelf == true) { ui_LoseScreen.SetActive(false); }

            player.ResetHealth();
            dealer.ResetHealth();

            isNewGame = false;
        }

        ui_Results.text = string.Empty;

        ui_Begin.SetActive(false);
        ui_PlayerHit.interactable = true;
        ui_PlayerStay.interactable = true;

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

            StartDealerTurn();
        }
    }

    public void StartDealerTurn()
    {
        ui_PlayerHit.interactable = false;
        ui_PlayerStay.interactable = false;

        StartCoroutine(DealDealerHand());
    }

    private IEnumerator DealDealerHand()
    {
        while (dealer.PlayersHandValue < 17)
        {
            deck.DealCard(dealer);

            yield return new WaitForSeconds(0.75f);
        }


        EndRound();

        ResetRound();
    }

    public void ResetRound()
    {
        ui_Begin.SetActive(true);
        ui_PlayerHit.interactable = false;
        ui_PlayerStay.interactable = false;

        //player.ResetHand();
        //dealer.ResetHand();

        //deck.RefreshDeck();
    }

    private void EndRound()
    {
        // Check to see if either player went bust;
        // If neither player is bust, determine who has the higher hand

        bool playerBust = player.IsPlayerBust;
        bool dealerBust = dealer.IsPlayerBust;

        int playerValue = player.PlayersHandValue;
        int dealerValue = dealer.PlayersHandValue;

        string message = "Default";

        if (playerBust == true && dealerBust == true)                                            // Both Bust
        {
            message = "It's an All Bust!";
            
        }
        else if (playerBust == true || (dealerBust == false && dealerValue > playerValue))       // Player busts; or, dealer has higher value
        {
            message = "The Clambler wins!";
            player.AdjustHealth((-1));
        }
        else if (dealerBust == true || playerValue > dealerValue)                        // Dealer busts; or, player has higher value
        {
            message = "You Win!";
            dealer.AdjustHealth((-1));
        }
        else if (playerValue == dealerValue)
        {
            message = "It's a Push! Bets returned!";
        }

        ui_Results.text = message;

        bool playerDead = player.IsPlayerDefeated;
        bool dealerDead = dealer.IsPlayerDefeated;

        Debug.Log(playerDead);
        Debug.Log(dealerDead);

        if (playerDead)
        {
            ResetRound();

            ui_LoseScreen.SetActive(true);

            isNewGame = true;
        }
        else if (dealerDead)
        {
            ResetRound();

            ui_WinScreen.SetActive(true);

            isNewGame = true;
        }
    }




}

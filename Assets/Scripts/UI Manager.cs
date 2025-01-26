using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
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
    [SerializeField] Button ui_Begin;
    [SerializeField] Button ui_Exit;

    [SerializeField] GameObject ui_WinScreen;
    [SerializeField] GameObject ui_LoseScreen;

    [SerializeField] Image ui_PlayersHand;
    [SerializeField] Image ui_PlayersThumb;

    [SerializeField] CardDeck deck;

    AudioSource audioSource;

    [SerializeField] AudioClip clip_DealCard;
    [SerializeField] AudioClip clip_Lose;
    [SerializeField] AudioClip clip_Win;
    [SerializeField] AudioClip clip_SuperLose;
    [SerializeField] AudioClip clip_SuperWin;
    [SerializeField] AudioClip clip_DealHand;

    bool isNewGame = true;

    //[SerializeField] bool playerBust;
    //[SerializeField] bool dealerBust;

    public void Start()
    {
        TogglePlayersHand(false);

        ui_PlayerHit.interactable = false;
        ui_PlayerStay.interactable = false;
        ui_Begin.interactable = true;
        ui_Exit.interactable = true;

        audioSource = GetComponent<AudioSource>();
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
        audioSource.PlayOneShot(clip_DealHand);

        if (isNewGame)
        {
            if (ui_WinScreen.activeSelf == true) { ui_WinScreen.SetActive(false); }
            if (ui_LoseScreen.activeSelf == true) { ui_LoseScreen.SetActive(false); }

           TogglePlayersHand(true);

            player.ResetHealth();
            dealer.ResetHealth();

            isNewGame = false;
        }

        ui_Results.text = string.Empty;

        ui_Begin.interactable = false;
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

        audioSource.PlayOneShot(clip_DealCard);

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

            audioSource.PlayOneShot(clip_DealCard);

            yield return new WaitForSeconds(0.75f);
        }


        EndRound();

        ResetRound();
    }

    public void ResetRound()
    {
        ui_Begin.interactable = true;
        ui_PlayerHit.interactable = false;
        ui_PlayerStay.interactable = false;

        //player.ResetHand();
        //dealer.ResetHand();

        //deck.RefreshDeck();
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(0); //Exit game
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
            message = "It's an All Bust! Tie!";
            audioSource.PlayOneShot(clip_Lose);

        }
        else if (playerBust == true || (dealerBust == false && dealerValue > playerValue))       // Player busts; or, dealer has higher value
        {
            message = "The Clambler wins!";

            audioSource.PlayOneShot(clip_Lose);

            player.AdjustHealth((-1));
        }
        else if (dealerBust == true || playerValue > dealerValue)                        // Dealer busts; or, player has higher value
        {
            message = "You Win!";

            audioSource.PlayOneShot(clip_Win);

            dealer.AdjustHealth((-1));
        }
        else if (playerValue == dealerValue)
        {
            message = "It's a Push! Tie!";

            audioSource.PlayOneShot(clip_Lose);
        }

        ui_Results.text = message;

        bool playerDead = player.IsPlayerDefeated;
        bool dealerDead = dealer.IsPlayerDefeated;

        if (playerDead)
        {
            ResetRound();

            TogglePlayersHand(false);

            ui_LoseScreen.SetActive(true);

            audioSource.PlayOneShot(clip_SuperLose);

            isNewGame = true;
        }
        else if (dealerDead)
        {
            ResetRound();

            TogglePlayersHand(false);

            ui_WinScreen.SetActive(true);

            audioSource.PlayOneShot(clip_SuperWin);

            isNewGame = true;
        }
    }

    private void TogglePlayersHand(bool toggle) 
    {
        ui_PlayersHand.enabled = toggle;
        ui_PlayersThumb.enabled = toggle;
    }




}

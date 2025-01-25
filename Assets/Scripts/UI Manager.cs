using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class UIManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Player dealer;

    [SerializeField] TextMeshProUGUI ui_PlayerScore;
    [SerializeField] TextMeshProUGUI ui_DealerScore;

    public void SetScoreDisplay(int displayToTarget)
    {
        int handScore;

        switch (displayToTarget) 
        {
            case 0:
                handScore = player.PlayersHandValue;
                ui_PlayerScore.text = handScore.ToString();
                break;
            case 1:
                handScore = dealer.PlayersHandValue;
                ui_DealerScore.text = handScore.ToString();
                break;
            default:
                Debug.Log("Error in SetScoreDisplay: Invalid switch case!");
                break;


        }
    }
}

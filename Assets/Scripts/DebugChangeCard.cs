using UnityEngine;

public class DebugChangeCard : MonoBehaviour
{
    CardModel cardModel;
    int cardIndex = 0;

    public GameObject card;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        cardModel = card.GetComponent<CardModel>();
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 28), "Hit me!"))
        {
            cardModel.cardIndex = cardIndex;
            cardModel.ToggelFace(true);

            cardIndex++;

            if (cardIndex == 52)
            {
                cardIndex = 0;
                cardModel.ToggelFace(false);
            }
        }
    }

}

using UnityEngine;

public class DebugChangeCard : MonoBehaviour
{
    Card cardModel;
    int cardIndex = 0;

    public GameObject card;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        cardModel = card.GetComponent<Card>();
    }

    private void OnGUI()
    {
        //if (GUI.Button(new Rect(10, 10, 100, 28), "Hit me!"))
        //{
            
        //    if (cardIndex >= cardModel.faces.Length)
        //    {
        //        cardIndex = 0;
        //        cardModel.ToggelFace(false);
        //    }
        //    else
        //    {
        //        cardModel.cardIndex = cardIndex;
        //        cardModel.ToggelFace(true);
        //    }
        //    cardIndex++;

        //}
    }

}

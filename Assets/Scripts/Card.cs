using UnityEngine;

public class Card : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public Sprite[] faces;
    public Sprite cardBack;

    [SerializeField] private int c_Rank;                                    // Card's value

    [SerializeField] private CardSuit c_Suit;                               // Card's suit

    public CardSuit Suit { get { return c_Suit; } }                         // Get Card Suit

    public int Rank { get { return c_Rank; } }                              // Get Card Rank

    public int cardIndex; //e.g faces[cardIndex]

    public void ToggelFace(bool showFace)
    {


        if (showFace)
        {
            spriteRenderer.sprite = faces[cardIndex]; //Show the card face
        }
        else
        {
            spriteRenderer.sprite = cardBack;//Show the card back
        }

    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}

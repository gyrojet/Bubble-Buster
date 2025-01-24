using UnityEngine;

public class Card : MonoBehaviour
{
    public enum Suit
    {
        Pearl,
        Kelp,
        Star,
        Fish
    }

    SpriteRenderer spriteRenderer;

    public Sprite face;
    public Sprite cardBack;

    [SerializeField] private int c_Rank;                                    // Card's value

    [SerializeField] private Suit c_Suit;                               // Card's suit

    public Suit CardSuit { get { return c_Suit; } }                         // Get Card Suit

    public int Rank { get { return c_Rank; } }                              // Get Card Rank

    public int cardIndex; //e.g faces[cardIndex]

    
}

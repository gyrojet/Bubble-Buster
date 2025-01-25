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

    public enum RankOfCard
    {
        Ace,
        King,
        Queen,
        Jack,
        Ten,
        Nine,
        Eight,
        Seven,
        Six,
        Five,
        Four,
        Three,
        Two
    }

    //SpriteRenderer spriteRenderer;

    public Sprite face;
    public Sprite cardBack;

    [SerializeField] private int c_Rank;                                    // Card's value

    [SerializeField] private Suit c_Suit;                                   // Card's suit

    [SerializeField] private RankOfCard c_RankOfCard;

    [SerializeField] private bool c_isAnAce;

    public Suit CardSuit { get { return c_Suit; } }                         // Get Card Suit

    public RankOfCard CardRankType { get { return c_RankOfCard; } }

    public int Rank { get { return c_Rank; } }                              // Get Card Rank

    public bool IsAnAce { get { return c_isAnAce;  } }

    public Sprite Face { get { return face; } } 

    //public void FlipCard()
    //{
    //    if (spriteRenderer.sprite == face)
    //        spriteRenderer.sprite = cardBack;
    //    else if (spriteRenderer.sprite == cardBack)
    //        spriteRenderer.sprite = face;
    //}
}

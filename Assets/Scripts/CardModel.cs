using UnityEngine;

public class CardModel : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public Sprite[] faces;
    public Sprite cardBack;

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

using UnityEngine;
using UnityEngine.UI;

public class CardBody : MonoBehaviour
{
    Image image;

    [SerializeField] private Sprite cardFace;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void SetFace(Sprite sprite)
    {
        cardFace = sprite;

        image.sprite = cardFace;
    }
}

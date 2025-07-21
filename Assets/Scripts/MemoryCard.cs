using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] private GameObject cardBack;
    [SerializeField] private SceneController controller;

    private int id;
    public int Id
    {
        get { return id; }
    }

    public void SetCard(int id, Sprite image)
    {
        this.id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    public void OnMouseDown()
    {
        if (cardBack.activeSelf && controller.canRevaled)
        {
            cardBack.SetActive(false);
            controller.CardRevaled(this);
        }
    }

    public void Unreval()
    {
        cardBack.SetActive(true);
    }
}

using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CardUI : MonoBehaviour
{
    public Image cardImage;
    public Text cardName;
    private Sprite cardBackground;
    public Sprite bgNotClicked;
    public Sprite bgClicked;
    public float cost;
    private bool isClicked = false;

    public PlayerKaijuScriptableObject cardData;
    private ShopUI shopUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void CreateCard(ShopUI shop)
    {
        cardBackground = bgNotClicked;
        Debug.Log("Creating card");
        cost = cardData.Cost;
        shopUI = shop;
        if(shopUI == null)
        {
            Debug.LogError("ShopUI is not assigned.");
        }
    }

    public void OnClick()
    {
        if (isClicked)
        {
            cardBackground = bgNotClicked;
            shopUI.OnCardDeselected(this);
        }
        else
        {
            cardBackground = bgClicked;
            shopUI.OnCardSelected(this);
        }
        isClicked = !isClicked;

    }
    private void Update()
    {
        if (cardBackground != null)
        {
            cardImage.sprite = cardBackground;
        }
        else
        {
            Debug.LogError("Card background sprite is not assigned.");
        }
    }
}

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public Image cardImage;
    public Text cardName;
    public float cost;

    public PlayerKaijuScriptableObject cardData;
    private ShopUI shopUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void CreateCard()
    {
        cost = cardData.Cost;
        
    }

    public void OnClick()
    {
        shopUI.OnCardPressed(this);
    }
}

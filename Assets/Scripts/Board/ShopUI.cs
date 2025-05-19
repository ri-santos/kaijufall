using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public PlayerManager playerManager;
    [SerializeField] private TextMeshProUGUI moneyText;

    void Start()
    {
        playerManager = PlayerManager.instance;
        playerManager.OnMoneyUpdated += UpdateMoney;
    }

    void Update()
    {
        
    }

    public void OnCardPressed(CardUI card)
    {
        if (playerManager.canBuy(card.cost))
        {
            playerManager.Buy(card.cost);
            Debug.Log("Card purchased: " + card.cardData.name); 
        }
        else
        {
            Debug.Log("Not enough money to buy this card.");
        }
    }

    private void UpdateMoney()
    {
        if (playerManager != null)
        {
            moneyText.text = playerManager.Money.ToString();
        }
        else
        {
            Debug.LogError("PlayerManager is not assigned.");
        }
    }
}

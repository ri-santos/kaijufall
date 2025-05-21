using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public PlayerManager playerManager;
    [SerializeField] private TextMeshProUGUI moneyText;
    PlayerKaijuSpawner spawner;
    //public PlayerKaijuScriptableObject cardData;
    //public CardUI cardPrefab;

    public List<CardUI> cards;

    private CardUI selectedCard;

    void Start()
    {
        playerManager = PlayerManager.instance;
        playerManager.OnMoneyUpdated += UpdateMoney;
        moneyText.text = playerManager.Money.ToString();
        spawner = GetComponent<PlayerKaijuSpawner>();
        spawner.OnKaijuSpawned += PurchaseKaiju;
        CreateCard();
        spawner = GetComponent<PlayerKaijuSpawner>();
        spawner.enabled = false;
    }

    public void CreateCard()
    {
        Debug.Log("Creating cards...");
        for (int i = 0; i < cards.Count; i++)
        {
            if (!cards[i].gameObject.activeSelf)
            {
                cards[i].gameObject.SetActive(true);
            }
            cards[i].CreateCard(this);
            //newCard.cardImage.sprite = cardData.Sprite;
        }
    }

    public void OnCardSelected(CardUI card)
    {
        if (selectedCard != null && selectedCard != card)
        {
            selectedCard.OnClick();
        }
        spawner.setKaiju(card.cardData.Prefab);
        spawner.enabled = true;
        selectedCard = card;
    }

    public void OnCardDeselected(CardUI card)
    {
        Debug.Log("Card deselected: " + card.cardData.name);
        spawner.setKaiju(null);
        spawner.enabled = false;
    }

    private void PurchaseKaiju()
    {
        if (selectedCard != null)
        {
            Debug.Log("Purchasing kaiju: " + selectedCard.cardData.name);
            if (playerManager.canBuy(selectedCard.cost))
            {
                playerManager.Buy(selectedCard.cost);
            }
            else
            {
                Debug.Log("Not enough money to purchase kaiju.");
            }
        }
    }

    private void UpdateMoney()
    {
        if (playerManager != null)
        {
            Debug.Log("Updating money display...");
            moneyText.text = playerManager.Money.ToString();
        }
        else
        {
            Debug.LogError("PlayerManager is not assigned.");
        }
    }
}

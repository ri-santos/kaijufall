using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public PlayerManager playerManager;
    [SerializeField] private TextMeshPro moneyText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerManager = FindAnyObjectByType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

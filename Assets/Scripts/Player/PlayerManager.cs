using NUnit.Framework;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private float dmgMult;
    public float DmgMult
    { get => dmgMult; private set => dmgMult = value; }

    [SerializeField]
    private float health;
    public float Health
    { get => health; private set => health = value; }

    [SerializeField]
    private float defense;
    public float Defense
    { get => defense; private set => defense = value; }

    [SerializeField]
    private float speed;
    public float Speed
    { get => speed; private set => speed = value; }

    [SerializeField]
    private float money;
    public float Money
    { get => money; private set => money = value; }

    public System.Action OnMoneyUpdated;

    #region Singleton
    public static PlayerManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Multiple instances of PlayerManager found. Destroying the new one.");
            Destroy(instance);
            return;
        }

        instance = this;
    }
    #endregion

    void Start()
    {
        money = 100; // Initialize money to 200
    }

    public bool canBuy(float cost)
    {
        return money >= cost;
    }

    public void Buy(float cost)
    {
        if (canBuy(cost))
        {
            money -= cost;
            OnUpdate();
        }
        else
        {
            Debug.Log("Not enough money to buy this item.");
        }
    }

    public void AddMoney(float amount)
    {
        money += amount;
        OnUpdate();
    }

    public void OnUpdate()
    {
        OnMoneyUpdated?.Invoke();
    }
}

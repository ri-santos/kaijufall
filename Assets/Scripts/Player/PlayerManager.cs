using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private float dmgMult;
    public float DmgMult
    { get => dmgMult; private set => dmgMult = value; }

    private float health;
    public float Health
    { get => health; private set => health = value; }

    private float defense;
    public float Defense
    { get => defense; private set => defense = value; }

    private float speed;
    public float Speed
    { get => speed; private set => speed = value; }

    private float money;
    public float Money
    { get => money; private set => money = value; }

    public System.Action OnMoneyUpdated;

    void Start()
    {
        money = 200; // Initialize money to 200
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
        }
        else
        {
            Debug.Log("Not enough money to buy this item.");
        }
    }

    public void OnUpdate()
    {
        OnMoneyUpdated?.Invoke();
    }
}

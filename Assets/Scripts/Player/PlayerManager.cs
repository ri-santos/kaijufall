using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public CharacterScriptableObject characterData;

    public System.Action OnMoneyUpdated;

    //current Stats
    //[HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentRecovery;
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentMight;
    [HideInInspector]
    public float currentProjectileSpeed;
    [HideInInspector]
    public float currentSouls;
    [HideInInspector]
    public float currentMagnet;

    //Exp and lvl
    [Header("Exp/Lvl")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;

    //Class for defining a level range and cap
    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    //I-Frames system
    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    public List<LevelRange> levelRanges;

    #region Singleton
    public static PlayerManager instance;

    private void Awake()
    {
        characterData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();
        if (instance != null)
        {
            Debug.LogWarning("Multiple instances of PlayerManager found. Destroying the new one.");
            Destroy(instance);
            return;
        }
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
        currentSouls = characterData.Souls;
        currentMagnet = characterData.Magnet;

        instance = this;
    }
    #endregion

    private void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;
    }

    private void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if (isInvincible)
        {
            isInvincible = false;
        }
        Recover();
    }
    public void IncreaseExperience(int amount)
    {
        experience = amount;
        LevelUpChecker();
    }

    void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;

            int experienceCapIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {
                if(level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;
        }
    }

    public void TakeDamage(float dmg)
    {
        if (!isInvincible)
        {
            currentHealth -= dmg;
            invincibilityTimer = invincibilityDuration;
            isInvincible = true;
            if (currentHealth <= 0)
            {
                Kill();
            }
        }
    }

    public void Kill()
    {
        Debug.Log("Player IS DEAD");
    }

    public bool canBuy(float cost)
    {
        return currentSouls >= cost;
    }

    public void Buy(float cost)
    {
        if (canBuy(cost))
        {
            currentSouls -= cost;
            OnUpdate();
        }
        else
        {
            Debug.Log("Not enough money to buy this item.");
        }
    }

    public void AddMoney(float amount)
    {
        currentSouls += amount;
        OnUpdate();
    }

    public void OnUpdate()
    {
        OnMoneyUpdated?.Invoke();
    }

    public float GetCurrentSouls()
    {
        return currentSouls;
    }

    public void RestoreHealth(int healthVal)
    {
        if(currentHealth < characterData.MaxHealth)
        {
            currentHealth += healthVal;

            if(currentHealth > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
        }
        
    }

    void Recover()
    {
        if(currentHealth <= characterData.MaxHealth)
        {
            currentHealth += currentRecovery * Time.deltaTime;

            if(currentHealth >= characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
        }
    }
}

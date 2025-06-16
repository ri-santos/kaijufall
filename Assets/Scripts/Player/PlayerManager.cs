using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework.Interfaces;
public class PlayerManager : MonoBehaviour
{
    CharacterData characterData;
    public CharacterData.Stats baseStats;
    [SerializeField] CharacterData.Stats actualStats;

    public System.Action OnMoneyUpdated;

    //current Stats
    //float currentHealth;
    //float currentRecovery;
    //float currentMoveSpeed;
    //float currentMight;
    //float currentProjectileSpeed;
    //float currentSouls;
    //float currentMagnet;

    public CharacterData.Stats Stats
    {
        get { return actualStats; }
        set
        {
            actualStats = value;
        }
    }

    float health;

    #region Current Stats Properties

    public ParticleSystem damageEffect; // Particle effect for damage feedback


    public float CurrentHealth
    {
        get { return health; }
        set
        {
            if(health != value)
            {
                health = value;
                UpdateHealthBar();

            }   
        }
    }

   
    #endregion

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

    PlayerCollector collector;
    PlayerInventory inventory;
    public int weaponIndex;
    public int passiveItemIndex;

    [Header("UI")]
    public Image healthBar;
    public Image expBar;
    public TMP_Text levelText;    

    //public GameObject secondWeaponTest;
    //public GameObject firstPassiveItemTest, secondPassiveItemTest;

    #region Singleton
    public static PlayerManager instance;

    private void Awake()
    {
        characterData = CharacterSelector.GetData();
        if (CharacterSelector.instance)
            CharacterSelector.instance.DestroySingleton();

        inventory = GetComponent<PlayerInventory>();
        collector = GetComponentInChildren<PlayerCollector>();
        baseStats = actualStats = characterData.stats;
        collector.SetRadius(actualStats.magnet);
        health = actualStats.maxHealth;

        if (instance != null)
        {
            Debug.LogWarning("Multiple instances of PlayerManager found. Destroying the new one.");
            Destroy(instance);
            return;
        }
        //CurrentHealth = characterData.MaxHealth;
        //CurrentRecovery = characterData.Recovery;
        //CurrentMoveSpeed = characterData.MoveSpeed;
        //CurrentMight = characterData.Might;
        //CurrentProjectileSpeed = characterData.ProjectileSpeed;
        //CurrentSouls = characterData.Souls;
        //CurrentMagnet = characterData.Magnet;

        ////second weapon and passive items are just for testing purposes, they can be removed later
        //SpawnWeapon(characterData.StartingWeapon);
        ////SpawnWeapon(secondWeaponTest);
        ////SpawnPassiveItem(firstPassiveItemTest);
        //SpawnPassiveItem(secondPassiveItemTest);


        instance = this;
    }
    #endregion

    private void Start()
    {
        inventory.Add(characterData.StartingWeapon);

        experienceCap = levelRanges[0].experienceCapIncrease;

        //GameManager.instance.currentHealthDisplay.text = "Health: " + Mathf.RoundToInt(CurrentHealth);
        

        GameManager.instance.AssignChosenCharacterUI(characterData);

        UpdateHealthBar();
        UpdateExpBar();
        UpdateLevelText();

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

    public void RecalculateStats() 
    {
        actualStats = baseStats;
        foreach (PlayerInventory.Slot s in inventory.passiveSlots)
        {
            Passive p = s.item as Passive;
            if (p)
            {
                actualStats += p.GetBoosts();
            }
        }
        collector.SetRadius(actualStats.magnet);
    }
    public void IncreaseExperience(int amount)
    {
        experience += amount;
        LevelUpChecker();
        UpdateExpBar();
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

            UpdateLevelText();

            GameManager.instance.StartLevelUp();
        }
    }

    void UpdateExpBar()
    {
        //update the bar the amount of experience
        expBar.fillAmount = (float)experience / experienceCap;
    }

    private void UpdateLevelText()
    {
        //update the level text
        levelText.text = "LVL " + level.ToString();
    }

    public void TakeDamage(float dmg)
    {
        if (!isInvincible)
        {
            CurrentHealth -= dmg;

            if (dmg > 0) GameManager.GenerateFloatingText(Mathf.FloorToInt(dmg).ToString(), transform);

            if (damageEffect) Destroy(Instantiate(damageEffect, transform.position, Quaternion.identity), 5f);

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;
            if (CurrentHealth <= 0)
            {
                Kill();
            }

            UpdateHealthBar();
        }
    }

    public void Dodge(float dodgeDuration)
    {
        if (!isInvincible)
        {
            invincibilityTimer = dodgeDuration;
            isInvincible = true;
        }
    }

     public void UpdateHealthBar()
    {
        //Update the health bar
        healthBar.fillAmount = CurrentHealth / actualStats.maxHealth;

    }


    public void Kill()
    {
        if (!GameManager.instance.isGameOver)
        {
            GameManager.instance.AssignLevelReachedUI(level);
            GameManager.instance.AssignChosenWeaponsAndPassiveItemsUI(inventory.weaponSlots, inventory.passiveSlots);
            GameManager.instance.GameOver();
        }
    }


    public bool canBuy(float cost)
    {
        return Stats.souls >= cost;
    }

    public void Buy(float cost)
    {
        if (canBuy(cost))
        {
            actualStats.souls -= cost;
            OnUpdate();
        }
        else
        {
            Debug.Log("Not enough money to buy this item.");
        }
    }

    public void AddMoney(float amount)
    {
        actualStats.souls += amount;
        OnUpdate();
    }

    public void OnUpdate()
    {
        OnMoneyUpdated?.Invoke();
    }

    public float GetCurrentSouls()
    {
        return Stats.souls;
    }

    public void RestoreHealth(int healthVal)
    {
        if(CurrentHealth < actualStats.maxHealth)
        {
            CurrentHealth += healthVal;

            if(CurrentHealth > actualStats.maxHealth)
            {
                CurrentHealth = actualStats.maxHealth;
            }

            //UpdateHealthBar();
        }
        
    }

    void Recover()
    {
        if (!GameManager.instance.isGameOver && !GameManager.instance.isPaused)
        {
            if (CurrentHealth < actualStats.maxHealth)
            {
                CurrentHealth += Stats.recovery * Time.deltaTime;
                CurrentHealth += Stats.recovery * Time.deltaTime;

                if (CurrentHealth >= actualStats.maxHealth)
                {
                    Debug.Log("Health is full, no need to recover further.");
                    CurrentHealth = actualStats.maxHealth;
                }

                //UpdateHealthBar();
            }
        }
    }

}

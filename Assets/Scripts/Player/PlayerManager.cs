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
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentHealthDisplay.text = string.Format("Health: {0}/{1}", Mathf.RoundToInt(health), actualStats.maxHealth);
                }

            }   
        }
    }

    public float MaxHealth
    {
        get { return actualStats.maxHealth; }
        set
        {
            if (actualStats.maxHealth != value)
            {
                actualStats.maxHealth = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentHealthDisplay.text = string.Format("Health: {0}/{1}", Mathf.RoundToInt(health), actualStats.maxHealth);
                }
            }
        }
    }

    public float CurrentRecovery
    {
        get { return Recovery; }
        set { Recovery = value; }
    }

    public float Recovery
    {
        get { return actualStats.recovery; }
        set
        {
            if(actualStats.recovery != value)
            {
                actualStats.recovery = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + actualStats.recovery;
                }
            }
        }
    }

    public float CurrentMoveSpeed
    {
        get { return MoveSpeed; }
        set { MoveSpeed = value; }
    }

    public float MoveSpeed
    {
        get { return actualStats.moveSpeed; }
        set
        {
            if(actualStats.moveSpeed != value)
            {
                actualStats.moveSpeed = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentMoveSpeedDisplay.text = "Move Speed: " + actualStats.moveSpeed;
                }
            }
        }
    }

    public float CurrentMight
    {
        get { return Might; }
        set { Might = value; }
    }

    public float Might
    {
        get { return actualStats.might; }
        set
        {
            if(actualStats.might != value)
            {
                actualStats.might = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentMightDisplay.text = "Might: " + actualStats.might;
                }
            }
        }
    }

    public float CurrentProjectileSpeed
    {
        get { return Speed; }
        set { Speed = value; }
    }

    public float Speed
    {
        get { return actualStats.speed; }
        set
        {
            if(actualStats.speed != value)
            {
                actualStats.speed = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentProjectileSpeedDisplay.text = "Projectile Speed: " + actualStats.speed;
                }
            }
        }
    }

    public float CurrentSouls
    {
        get { return Souls; }
        set { Souls = value; }
    }

    public float Souls
    {
        get { return actualStats.souls; }
        set
        {
            if(actualStats.souls != value)
            {
                actualStats.souls = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentSoulsDisplay.text = "Souls: " + actualStats.souls;
                }
            }
        }
    }

    public float CurrentMagnet
    {
        get { return Magnet; }
        set { Magnet = value; }
    }

    public float Magnet
    {
        get { return actualStats.magnet; }
        set
        {
            if(actualStats.magnet != value)
            {
                actualStats.magnet = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentMagnetDisplay.text = "Magnet: " + actualStats.magnet;
                }
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
        if(CharacterSelector.instance)
            CharacterSelector.instance.DestroySingleton();

        inventory = GetComponent<PlayerInventory>();
        baseStats = actualStats = characterData.stats;
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

        GameManager.instance.currentHealthDisplay.text = "Health: " + Mathf.RoundToInt(CurrentHealth);
        GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + CurrentRecovery;
        GameManager.instance.currentMoveSpeedDisplay.text = "Move Speed: " + CurrentMoveSpeed;
        GameManager.instance.currentMightDisplay.text = "Might: " + CurrentMight;
        GameManager.instance.currentProjectileSpeedDisplay.text = "Projectile Speed: " + CurrentProjectileSpeed;
        GameManager.instance.currentSoulsDisplay.text = "Souls: " + CurrentSouls;
        GameManager.instance.currentMagnetDisplay.text = "Magnet: " + CurrentMagnet;

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
        return CurrentSouls >= cost;
    }

    public void Buy(float cost)
    {
        if (canBuy(cost))
        {
            CurrentSouls -= cost;
            OnUpdate();
        }
        else
        {
            Debug.Log("Not enough money to buy this item.");
        }
    }

    public void AddMoney(float amount)
    {
        CurrentSouls += amount;
        OnUpdate();
    }

    public void OnUpdate()
    {
        OnMoneyUpdated?.Invoke();
    }

    public float GetCurrentSouls()
    {
        return CurrentSouls;
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
        }
        
    }

    void Recover()
    {
        if (!GameManager.instance.isGameOver && !GameManager.instance.isPaused)
        {
            if (CurrentHealth < actualStats.maxHealth)
            {
                CurrentHealth += CurrentRecovery * Time.deltaTime;
                CurrentHealth += Recovery * Time.deltaTime;

                if (CurrentHealth >= actualStats.maxHealth)
                {
                    Debug.Log("Health is full, no need to recover further.");
                    CurrentHealth = actualStats.maxHealth;
                }
            }
        }
    }

    //[System.Obsolete("old function that is kept to maintain compatibility with the InventoryManager")]
    //public void SpawnWeapon(GameObject weapon)
    //{
    //    if(weaponIndex >= inventory.weaponSlots.Count - 1)
    //    {
    //        Debug.LogWarning("No more weapon slots available to spawn a new weapon.");
    //        return;
    //    }
    //    GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
    //    spawnedWeapon.transform.SetParent(transform);
    //    //inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>());
        
    //    weaponIndex++;
    //}
    //[System.Obsolete("old function that is kept to maintain compatibility with the InventoryManager")]
    //public void SpawnPassiveItem(GameObject passiveItem)
    //{
    //    //if (passiveItemIndex >= inventory.passiveItemSlots.Count - 1)
    //    //{
    //    //    Debug.LogWarning("No more passive Item slots available.");
    //    //    return;
    //    //}
    //    GameObject spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
    //    spawnedPassiveItem.transform.SetParent(transform);
    //    //inventory.AddPassiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItem>());

    //    passiveItemIndex++;
    //}
}

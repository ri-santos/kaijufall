using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public CharacterScriptableObject characterData;

    public System.Action OnMoneyUpdated;

    //current Stats
    float currentHealth;
    float currentRecovery;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileSpeed;
    float currentSouls;
    float currentMagnet;

    #region Current Stats Properties
    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            if(currentHealth != value)
            {
                currentHealth = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentHealthDisplay.text = "Health: " + currentHealth;
                }

            }   
        }
    }

    public float CurrentRecovery
    {
        get { return currentRecovery; }
        set
        {
            if(currentRecovery != value)
            {
                currentRecovery = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + currentRecovery;
                }
            }
        }
    }

    public float CurrentMoveSpeed
    {
        get { return currentMoveSpeed; }
        set
        {
            if(currentMoveSpeed != value)
            {
                currentMoveSpeed = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentMoveSpeedDisplay.text = "Move Speed: " + currentMoveSpeed;
                }
            }
        }
    }

    public float CurrentMight
    {
        get { return currentMight; }
        set
        {
            if(currentMight != value)
            {
                currentMight = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentMightDisplay.text = "Might: " + currentMight;
                }
            }
        }
    }

    public float CurrentProjectileSpeed
    {
        get { return currentProjectileSpeed; }
        set
        {
            if(currentProjectileSpeed != value)
            {
                currentProjectileSpeed = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentProjectileSpeedDisplay.text = "Projectile Speed: " + currentProjectileSpeed;
                }
            }
        }
    }

    public float CurrentSouls
    {
        get { return currentSouls; }
        set
        {
            if(currentSouls != value)
            {
                currentSouls = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentSoulsDisplay.text = "Souls: " + currentSouls;
                }
            }
        }
    }

    public float CurrentMagnet
    {
        get { return currentMagnet; }
        set
        {
            if(currentMagnet != value)
            {
                currentMagnet = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentMagnetDisplay.text = "Magnet: " + currentMagnet;
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

    InventoryManager inventory;
    public int weaponIndex;
    public int passiveItemIndex;

    [Header("UI")]
    public Image healthBar;
    public Image expBar;
    public Text levelText;

    public GameObject secondWeaponTest;
    public GameObject firstPassiveItemTest, secondPassiveItemTest;

    #region Singleton
    public static PlayerManager instance;

    private void Awake()
    {
        characterData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();

        inventory = GetComponent<InventoryManager>();

        if (instance != null)
        {
            Debug.LogWarning("Multiple instances of PlayerManager found. Destroying the new one.");
            Destroy(instance);
            return;
        }
        CurrentHealth = characterData.MaxHealth;
        CurrentRecovery = characterData.Recovery;
        CurrentMoveSpeed = characterData.MoveSpeed;
        CurrentMight = characterData.Might;
        CurrentProjectileSpeed = characterData.ProjectileSpeed;
        CurrentSouls = characterData.Souls;
        CurrentMagnet = characterData.Magnet;

        //second weapon and passive items are just for testing purposes, they can be removed later
        SpawnWeapon(characterData.StartingWeapon);
        //SpawnWeapon(secondWeaponTest);
        //SpawnPassiveItem(firstPassiveItemTest);
        SpawnPassiveItem(secondPassiveItemTest);


        instance = this;
    }
    #endregion

    private void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;

        GameManager.instance.currentHealthDisplay.text = "Health: " + CurrentHealth;
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
            invincibilityTimer = invincibilityDuration;
            isInvincible = true;
            if (CurrentHealth <= 0)
            {
                Kill();
            }

            UpdateHealthBar();
        }
    }

     public void UpdateHealthBar()
    {
        //Update the health bar
        healthBar.fillAmount = currentHealth / characterData.MaxHealth;

    }


    public void Kill()
    {
        if (!GameManager.instance.isGameOver)
        {
            GameManager.instance.AssignLevelReachedUI(level);
            GameManager.instance.AssignChosenWeaponsAndPassiveItemsUI(inventory.weaponUISlots, inventory.passiveItemUISlots);
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
        if(CurrentHealth < characterData.MaxHealth)
        {
            CurrentHealth += healthVal;

            if(CurrentHealth > characterData.MaxHealth)
            {
                CurrentHealth = characterData.MaxHealth;
            }
        }
        
    }

    void Recover()
    {
        if(CurrentHealth <= characterData.MaxHealth)
        {
            CurrentHealth += CurrentRecovery * Time.deltaTime;

            if(CurrentHealth >= characterData.MaxHealth)
            {
                CurrentHealth = characterData.MaxHealth;
            }
        }
    }

    public void SpawnWeapon(GameObject weapon)
    {
        if(weaponIndex >= inventory.weaponSlots.Count - 1)
        {
            Debug.LogWarning("No more weapon slots available to spawn a new weapon.");
            return;
        }
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>());
        
        weaponIndex++;
    }
    public void SpawnPassiveItem(GameObject passiveItem)
    {
        if (passiveItemIndex >= inventory.passiveItemSlots.Count - 1)
        {
            Debug.LogWarning("No more passive Item slots available.");
            return;
        }
        GameObject spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnedPassiveItem.transform.SetParent(transform);
        inventory.AddPassiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItem>());

        passiveItemIndex++;
    }
}

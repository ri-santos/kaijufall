using UnityEngine;

public class PlayerKaijuStats : MonoBehaviour
{
    public PlayerKaijuScriptableObject kaijuData;

    private float currentSpeed;
    private float currentHealth;
    private float currentDamage;

    //I-Frames system
    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    private void Awake()
    {
        currentSpeed = kaijuData.Speed;
        currentHealth = kaijuData.Health;
        currentDamage = kaijuData.Damage;
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
        Destroy(gameObject);
    }
}

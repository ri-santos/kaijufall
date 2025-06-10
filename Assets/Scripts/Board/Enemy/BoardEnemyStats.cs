using UnityEngine;

public class BoardEnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    private float currentSpeed;
    private float currentHealth;
    private float currentDamage;
    public float reward;

    //I-Frames system
    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    private void Awake()
    {
        currentSpeed = enemyData.Speed;
        currentHealth = enemyData.Health;
        currentDamage = enemyData.Damage;
        reward = enemyData.Reward;
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {   
        Debug.Log("Player Hit");
        if (collision.gameObject.CompareTag("Player"))
        {
            
            PlayerKaijuStats player = collision.gameObject.GetComponent<PlayerKaijuStats>();
            player.TakeDamage(currentDamage);
        }
    }
}

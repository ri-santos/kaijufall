using UnityEditor;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    
    private float currentSpeed;
    private float currentHealth;
    private float currentDamage;

    private void Awake()
    {
        currentSpeed = enemyData.Speed;
        currentHealth = enemyData.Health;
        currentDamage = enemyData.Damage;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Current Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}

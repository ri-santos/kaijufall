using UnityEngine;

public class BigKaiju : MonoBehaviour  // Must inherit from MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Add death effects here
        Destroy(gameObject);
    }
}
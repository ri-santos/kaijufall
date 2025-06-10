using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;
    public event System.Action OnDeath;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}
using UnityEngine;

public class BigKaiju : MonoBehaviour
{
    public BigKaijuScriptableObject kaijuData;
    private float currentHealth;

    private void Start()
    {
        currentHealth = kaijuData.Health;

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

    public void SpawnEnemies()
    {
        for (int i = 0; i < kaijuData.NumEnemies; i++)
        {
            Vector3 spawnPos;
            if (i % 2 == 0)
            {
                spawnPos = new Vector3(transform.position.x + Random.Range(-7f, 0f), transform.position.y + Random.Range(0f, 2f), 0);
            }
            else
            {
                spawnPos = new Vector3(transform.position.x + Random.Range(0f, 7f), transform.position.y + Random.Range(0f, 2f), 0);
            }
            GameObject enemy = Instantiate(kaijuData.enemyPrefab, spawnPos, Quaternion.identity);
            enemy.transform.SetParent(transform);
        }
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}

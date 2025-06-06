using TMPro;
using UnityEngine;

public class BigKaiju : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hpDisplay;
    public BigKaijuScriptableObject kaijuData;
    private float currentHealth;
    [SerializeField] private int rangeNumEnemies;
    [SerializeField] private GameObject[] enemies;

    private void Start()
    {
        currentHealth = kaijuData.Health;
        rangeNumEnemies = Random.Range(-rangeNumEnemies, rangeNumEnemies);
        hpDisplay.text = "Big Kaiju HP: " + currentHealth.ToString("F0");
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Current Health: " + currentHealth);
        hpDisplay.text = "Big Kaiju HP: " + currentHealth.ToString("F0");
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < kaijuData.NumEnemies + rangeNumEnemies; i++)
        {
            int type = Random.Range(0, enemies.Length);
            Debug.Log(type);
            Vector3 spawnPos;
            if (i % 2 == 0)
            {
                spawnPos = new Vector3(transform.position.x + Random.Range(-10f, -2f), transform.position.y + Random.Range(0f, 3f), 0);
            }
            else
            {
                spawnPos = new Vector3(transform.position.x + Random.Range(2f, 10f), transform.position.y + Random.Range(0f, 3f), 0);
            }
            GameObject enemy = Instantiate(enemies[type], spawnPos, Quaternion.identity);
            enemy.transform.SetParent(transform);
        }
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}

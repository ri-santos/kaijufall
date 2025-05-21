using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float timeBetweenSpawns;
    private float timeSinceLastSpawn;
    private PhaseManager phaseManager;

    private void Start()
    {
        timeSinceLastSpawn = 0f;
        FindFirstObjectByType<PhaseManager>();
    }

    void FixedUpdate()
    {
        if (timeSinceLastSpawn < timeBetweenSpawns)
        {
            timeSinceLastSpawn += Time.deltaTime;
        }
        else
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0f;
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        
        // Add health component if not present
        if (!newEnemy.TryGetComponent(out EnemyHealth health))
        {
            health = newEnemy.AddComponent<EnemyHealth>();
        }
        
        // Register death event
        health.OnDeath += () => phaseManager.RegisterKaijuDefeat();
    }
}
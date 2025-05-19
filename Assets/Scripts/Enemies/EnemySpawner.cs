using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] float timeBetweenSpawns;

    float timeSinceLastSpawn;

    private void Start()
    {
        timeSinceLastSpawn = 0f; // Initialize the spawn timer
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timeSinceLastSpawn < timeBetweenSpawns)
        {
            timeSinceLastSpawn += Time.deltaTime;
        }
        else
        {
            Debug.Log("Spawning enemy");
            SpawnEnemy();
            timeSinceLastSpawn = 0f; // Reset the spawn timer
        }
    }
    void SpawnEnemy()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}

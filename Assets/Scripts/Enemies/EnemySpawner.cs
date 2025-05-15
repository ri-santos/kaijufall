using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] float timeBetweenSpawns = 2f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timeBetweenSpawns > 0)
        {
            timeBetweenSpawns -= Time.deltaTime;
        }
        else
        {
            SpawnEnemy();
            timeBetweenSpawns = 2f;
        }
    }
    void SpawnEnemy()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}

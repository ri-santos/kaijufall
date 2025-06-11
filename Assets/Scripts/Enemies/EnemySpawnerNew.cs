using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerNew : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups; //list of groups to spawn
        public int waveQuota; //number to spawn
        public float spawnInterval; //interval to spawn
        public int spawnCount; //already spawned
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount; //nm of enemies to spawn in this wave
        public int spawnCount; // nm of enemies of this type already spawned
        public GameObject enemyPrefab;
    }

    public List<Wave> waves; //all the waves in the game
    public int currentWaveCount; //index of current wave [list starts from 0]

    [Header("Spawner Attributes")]
    float spawnTimer;
    public int enemiesAlive;
    public int maxEnemiesAllowed;
    public bool maxEnemiesReached;
    public float waveInterval;
    bool isWaveActive = false;

    [Header("Spawn Positions")]
    public List<Transform> relativeSpawnPoints;



    Transform player;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerManager>().transform;
        CalculateWaveQuota();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0 && !isWaveActive)
        {
            StartCoroutine(BeginNextWave());
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= waves[currentWaveCount].spawnInterval) 
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    IEnumerator BeginNextWave()
    {
        isWaveActive = true;

        //wave for 'waveInterval' seconds before starting the next wave
        yield return new WaitForSeconds(waveInterval);

        //if there are more waves art the next wave
        if (currentWaveCount < waves.Count - 1)
        {
            isWaveActive = false;
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach(var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
        Debug.LogWarning(currentWaveQuota);
    }

    void SpawnEnemies()
    {
        //Check if the minimum number of enemies for the current wave has been reached
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            foreach(var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                if(enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    GameObject enemy = Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);
                    enemy.transform.SetParent(transform); // Set the parent to the spawner for organization

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;

                    if (enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }
                }
            }
        }
    }

    public void OnEnemyKilled()
    {
        // Decrease the count of enemies alive and update the wave's spawn count
        enemiesAlive--;

        //reset the maxEnemiesReached flag if the number of alive enemies is less than the allowed maximum
        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }
}

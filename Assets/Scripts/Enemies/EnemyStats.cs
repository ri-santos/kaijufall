using UnityEditor;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    
    [HideInInspector]
    public float currentSpeed;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentDamage;
    [HideInInspector]
    public float reward;

    public float despawnDistance = 20f;
    Transform player;

    private void Awake()
    {
        currentSpeed = enemyData.Speed;
        currentHealth = enemyData.Health;
        currentDamage = enemyData.Damage;
        reward = enemyData.Reward;
    }

    private void Start()
    {
        player = FindAnyObjectByType<Player>().transform;

    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager player = collision.gameObject.GetComponent<PlayerManager>();
            player.TakeDamage(currentDamage);
        }
    }

    private void OnDestroy()
    {
        EnemySpawnerNew es = FindAnyObjectByType<EnemySpawnerNew>();
        es.OnEnemyKilled();
    }

    void ReturnEnemy()
    {
        EnemySpawnerNew es = FindAnyObjectByType<EnemySpawnerNew>();
        transform.position = player.position + es.relativeSpawnPoints[Random.Range(0,es.relativeSpawnPoints.Count)].position;
    }
}

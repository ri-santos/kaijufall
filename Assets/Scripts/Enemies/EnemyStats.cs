using System.Collections;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
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

    [Header("Damage Feedback")]
    public Color damageColor = new Color(1,0,0,1); //what color to flash when taking damage
    public float damageFlashDuration = 0.2f; // how long to flash the damage color
    public float deathFadeTime = 0.6f; // how long to fade out the enemy on death
    Color originalColor; // the original color of the enemy sprite
    SpriteRenderer sr;
    EnemyMovement movement;

    private void Awake()
    {
        currentSpeed = enemyData.Speed;
        currentHealth = enemyData.Health;
        currentDamage = enemyData.Damage;
        reward = enemyData.Reward;
    }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color; // Store the original color of the sprite

        movement = GetComponent<EnemyMovement>();

    }

    private void Update()
    {
        Player target = FindAnyObjectByType<Player>();
        if (target == null) return; // If no player is found, exit the update
        
        player = target.transform;
        
        if (Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
    }

    public void TakeDamage(float damage, Vector2 sourcePosition, float knockbackForce = 5f, float knockbackDuration = 0.2f)
    {
        currentHealth -= damage;
        StartCoroutine(DamageFlash()); // Start the damage flash coroutine

        if(damage > 0) GameManager.GenerateFloatingText(Mathf.FloorToInt(damage).ToString(), transform);

        if (knockbackForce > 0)
        {
            //gets the direction of the knockback
            Vector2 dir = (Vector2)transform.position - sourcePosition;
            movement.Knockback(dir.normalized * knockbackForce, knockbackDuration); // Apply knockback to the enemy
        }
        //Debug.Log("Current Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    //coroutine to flash the enemy sprite when it takes damage

    IEnumerator DamageFlash()
    {
        sr.color = damageColor; // Change the sprite color to the damage color
        yield return new WaitForSeconds(damageFlashDuration); // Wait for the specified duration
        sr.color = originalColor; // Restore the original color
    }

    public void Kill()
    {

        StartCoroutine(KillFade());
    }

    // Coroutine to fade out the enemy sprite on death
    IEnumerator KillFade()
    {
        WaitForEndOfFrame w = new WaitForEndOfFrame();
        float t = 0, origAlpha = sr.color.a;

        while(t<deathFadeTime)
        {
            yield return w;
            t += Time.deltaTime;

            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, (1 -t / deathFadeTime) * origAlpha);
        }

        Destroy(gameObject); // Destroy the enemy object after fading out
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

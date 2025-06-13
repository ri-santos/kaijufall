using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BoardEnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    private float currentSpeed;
    private float currentHealth;
    private float currentDamage;
    public float reward;

    [Header("Damage Feedback")]
    public Color damageColor = new Color(1, 0, 0, 1); //what color to flash when taking damage
    public float damageFlashDuration = 0.2f; // how long to flash the damage color
    public float deathFadeTime = 0.3f; // how long to fade out the enemy on death
    Color originalColor; // the original color of the enemy sprite
    SpriteRenderer sr;

    //I-Frames system
    //[Header("I-Frames")]
    //public float invincibilityDuration;
    //float invincibilityTimer;
    //bool isInvincible;

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
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        StartCoroutine(DamageFlash()); // Start the damage flash coroutine
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

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

        while (t < deathFadeTime)
        {
            yield return w;
            t += Time.deltaTime;

            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, (1 - t / deathFadeTime) * origAlpha);
        }

        Destroy(gameObject); // Destroy the enemy object after fading out
    }

    private void OnCollisionStay2D(Collision2D collision)
    {   
        Debug.Log("Player Hit");
        if (collision.gameObject.CompareTag("Player"))
        {
            
            PlayerKaijuStats player = collision.gameObject.GetComponent<PlayerKaijuStats>();
            player.TakeDamage(currentDamage);
        }
    }
}

using TMPro;
using UnityEngine;
using System.Collections;

public class BigKaiju : MonoBehaviour, IDamageable
{
    [SerializeField] private TextMeshProUGUI hpDisplay;
    public BigKaijuScriptableObject kaijuData;
    private float currentHealth;
    private Transform target;
    public float CurrentHealth => currentHealth;
    [SerializeField] private int rangeNumEnemies;
    [SerializeField] private GameObject[] enemies;

    private BigKaijuBoardAttackController boardAttackController;
    private BigKaijuFinalAttackController finalAttackController;
    private BigKaijuMovement kaijuMovement;

    [Header("Damage Feedback")]
    public Color damageColor = new Color(1, 0, 0, 1); //what color to flash when taking damage
    public float damageFlashDuration = 0.2f; // how long to flash the damage color
    public float deathFadeTime = 0.6f; // how long to fade out the enemy on death
    Color originalColor; // the original color of the enemy sprite
    SpriteRenderer sr;

    private void Start()
    {
        boardAttackController = GetComponent<BigKaijuBoardAttackController>();
        finalAttackController = GetComponent<BigKaijuFinalAttackController>();
        kaijuMovement = GetComponent<BigKaijuMovement>();
        target = finalAttackController.Target;

        boardAttackController.enabled = false;
        finalAttackController.enabled = false;
        kaijuMovement.enabled = false;

        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;

        currentHealth = kaijuData.Health;
        rangeNumEnemies = Random.Range(-rangeNumEnemies, rangeNumEnemies);
        hpDisplay.text = "Big Kaiju HP: " + currentHealth.ToString("F0");
        GameManager.instance.onChangeToFinal += ChangeToFinalPhase;
        GameManager.instance.onChangeToPlayer += EndBoardAttackPhase;
    }

    private void Update()
    {
        if (GameManager.instance.currentState == GameManager.GameState.Final)
        {
            if (FindFirstObjectByType<Player>() == null) return;
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, kaijuData.Speed * Time.deltaTime);
        }
    }

    public void TakeDamage(float damage, Vector2 sourcePosition, float knockbackForce = 5f, float knockbackDuration = 0.2f)
    {
        currentHealth -= damage;
        StartCoroutine(DamageFlash());
        if (damage > 0) GameManager.GenerateFloatingText(Mathf.FloorToInt(damage).ToString(), transform);
        Debug.Log("Current Health: " + currentHealth);
        hpDisplay.text = "Big Kaiju HP: " + currentHealth.ToString("F0");
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

    public void SpawnEnemies()
    {
        for (int i = 0; i < kaijuData.NumEnemies + rangeNumEnemies; i++)
        {
            int type = Random.Range(0, enemies.Length);
            Debug.Log(type);
            Vector3 spawnPos;
            if (i % 2 == 0)
            {
                spawnPos = new Vector3(transform.position.x + Random.Range(-10f, -2f), transform.position.y + Random.Range(-2f, -4f), 0);
            }
            else
            {
                spawnPos = new Vector3(transform.position.x + Random.Range(2f, 10f), transform.position.y + Random.Range(-2f, -4f), 0);
            }
            GameObject enemy = Instantiate(enemies[type], spawnPos, Quaternion.identity);
            enemy.transform.SetParent(transform);
        }
    }

    public void StartBoardAttackPhase()
    {
        boardAttackController.enabled = true;
    }

    private void EndBoardAttackPhase()
    {
        boardAttackController.enabled = false;
    }

    private void ChangeToFinalPhase()
    {
        Vector3 targetPos = target.position;
        transform.position = new Vector3(targetPos.x, targetPos.y + 10, targetPos.z);
        boardAttackController.enabled = false;
        finalAttackController.enabled = true;
        kaijuMovement.enabled = true;
    }

    void Kill()
    {
        StartCoroutine(KillFade());
        Destroy(gameObject);
        GameManager.instance.GameOver();
    }

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
}

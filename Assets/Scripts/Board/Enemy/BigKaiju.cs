using TMPro;
using UnityEngine;

public class BigKaiju : MonoBehaviour
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

    private void Start()
    {
        boardAttackController = GetComponent<BigKaijuBoardAttackController>();
        finalAttackController = GetComponent<BigKaijuFinalAttackController>();
        kaijuMovement = GetComponent<BigKaijuMovement>();
        target = finalAttackController.Target;

        boardAttackController.enabled = false;
        finalAttackController.enabled = false;
        kaijuMovement.enabled = false;

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
        Destroy(gameObject);
        GameManager.instance.GameOver();
    }
}

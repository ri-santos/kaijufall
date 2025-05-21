using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    Transform target;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (target == null) 
        {
            target = GameObject.FindWithTag("Player")?.transform;
            if (target == null)
                Debug.LogWarning("Enemy target not found!");
        }
        target = FindFirstObjectByType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, enemyData.Speed * Time.deltaTime);
    }
}

using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform target;
    EnemyStats enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        target = FindFirstObjectByType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, enemy.currentSpeed * Time.deltaTime);
    }
}

using UnityEngine;

public class BoardEnemyMovement : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = FindFirstObjectByType<PlayerKaijuMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, enemyData.Speed * Time.deltaTime);
    }
}

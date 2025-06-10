using UnityEngine;

public class BoardEnemyMovement : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    private BoardEnemyKaijuAttackController attackController;
    PlayerKaijuStats target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackController = GetComponent<BoardEnemyKaijuAttackController>();
    }

    // Update is called once per frame
    void Update()
    {
        target = attackController.Target;

        if(!attackController.InRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, enemyData.Speed * Time.deltaTime);
        }
    }
}

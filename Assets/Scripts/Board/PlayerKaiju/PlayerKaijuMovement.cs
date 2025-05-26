using UnityEngine;

public class PlayerKaijuMovement : MonoBehaviour
{
    public PlayerKaijuScriptableObject playerKaijuData;
    Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target = FindFirstObjectByType<BoardEnemyMovement>().transform;
        if (target == null)
        {
            target = FindFirstObjectByType<BigKaiju>().transform;
        }
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, playerKaijuData.Speed * Time.deltaTime);
    }
}

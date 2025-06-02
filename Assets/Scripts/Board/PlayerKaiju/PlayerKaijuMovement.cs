using UnityEngine;

public class PlayerKaijuMovement : MonoBehaviour
{
    public PlayerKaijuScriptableObject playerKaijuData;
    private PlayerKaijuAttackController attackController;
    GameObject target;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackController = GetComponent<PlayerKaijuAttackController>();
    }

    // Update is called once per frame
    void Update()
    {
        target = attackController.target;
        if (!attackController.CanAttack)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, playerKaijuData.Speed * Time.deltaTime);
        }
    }
}

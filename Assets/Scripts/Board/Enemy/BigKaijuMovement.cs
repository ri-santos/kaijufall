using UnityEngine;

public class BigKaijuMovement : MonoBehaviour
{
    public BigKaijuScriptableObject kaijuData;
    private BigKaijuFinalAttackController attackController;
    Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackController = GetComponent<BigKaijuFinalAttackController>();
    }

    // Update is called once per frame
    void Update()
    {
        target = attackController.Target;

        if (target != null && !attackController.InRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, kaijuData.Speed * Time.deltaTime);
        }
    }
}

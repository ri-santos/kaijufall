using UnityEngine;

[RequireComponent(typeof(PlayerKaijuAttackController))]
public class PlayerKaijuMovement : MonoBehaviour
{
    public PlayerKaijuScriptableObject playerKaijuData;
    private BigKaiju target; // Changed from Transform to BigKaiju
    private PlayerKaijuAttackController attackController;
    
    private void Awake()
    {
        attackController = GetComponent<PlayerKaijuAttackController>();
        target = FindAnyObjectByType<BigKaiju>();
    }
    
    private void Update()
    {
        if (target == null || attackController == null) return;
        
        if (!attackController.CanAttack)
        {
            // Access target's transform explicitly
            transform.position = Vector2.MoveTowards(
                transform.position, 
                target.transform.position, // Changed to target.transform.position
                playerKaijuData.Speed * Time.deltaTime);
        }
    }
}
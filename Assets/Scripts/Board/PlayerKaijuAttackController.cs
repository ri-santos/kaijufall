using UnityEngine;

[RequireComponent(typeof(PlayerKaijuMovement))]
public class PlayerKaijuAttackController : MonoBehaviour
{
    [SerializeField] private PlayerKaijuScriptableObject kaijuData;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 1f;
    
    private float currentAttackCooldown;
    private BigKaiju target;
    public bool CanAttack { get; private set; } // Added public property
    
    private void Awake()
    {
        target = FindAnyObjectByType<BigKaiju>(); // Updated to non-obsolete method
    }
    
    private void Update()
    {
        if (target == null) return;
        
        float distance = Vector2.Distance(transform.position, target.transform.position);
        CanAttack = distance <= kaijuData.AttackRange;
        
        if (currentAttackCooldown > 0)
        {
            currentAttackCooldown -= Time.deltaTime;
            return;
        }
        
        if (CanAttack)
        {
            Attack();
            currentAttackCooldown = kaijuData.AttackCooldown;
        }
    }
    
    private void Attack()
    {
        if (target == null) return;
        
        // Apply damage
        target.TakeDamage(kaijuData.Damage);
        
        // Visual feedback
        Debug.Log($"{kaijuData.KaijuName} attacked! Damage: {kaijuData.Damage}");
        
        // Cooldown
        currentAttackCooldown = kaijuData.AttackCooldown;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
using UnityEngine;

public class BoardEnemyKaijuAttackController : MonoBehaviour
{
    [SerializeField] protected EnemyScriptableObject kaijuData;
    [SerializeField] protected Transform attackPoint;
    protected float attackRadius;

    protected float currentAttackCooldown;
    protected PlayerKaijuStats target;
    public PlayerKaijuStats Target => target;

    protected bool inCooldown;
    public bool InCooldown => inCooldown; // Public property to access canAttack

    protected bool inRange;

    public bool InRange => inRange; // Public property to access inRange

    protected bool canAttack => inRange && !inCooldown;

    protected virtual void Awake()
    {
        target = FindAnyObjectByType<PlayerKaijuStats>(); // Updated to non-obsolete method
        attackPoint = transform; // Use the transform if attackPoint is not set
        attackRadius = kaijuData.AttackRange; // Set attack radius from kaiju data
    }

    protected virtual void Update()
    {
        target = FindFirstObjectByType<PlayerKaijuStats>(); // Updated to non-obsolete method
        if (target == null) return;

        float distance = Vector2.Distance(transform.position, target.transform.position);
        inRange = distance <= kaijuData.AttackRange;

        currentAttackCooldown -= Time.deltaTime;
        inCooldown = currentAttackCooldown > 0;

        if (canAttack)
        {
            Attack();
            // Reset cooldown after attack
            currentAttackCooldown = kaijuData.AttackCooldown;
        }
    }

    protected virtual void Attack()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}

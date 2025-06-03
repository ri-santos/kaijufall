using UnityEngine;

public class BoardEnemyKaijuAttackController : MonoBehaviour
{
    [SerializeField] protected EnemyScriptableObject kaijuData;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected float attackRadius = 1f;

    protected float currentAttackCooldown;
    protected PlayerKaijuStats target { get; private set; }

    protected bool inCooldown;
    public bool InCooldown => inCooldown; // Public property to access canAttack

    protected bool inRange;

    public bool InRange => inRange; // Public property to access inRange

    protected bool canAttack => inRange && !inCooldown;

    protected virtual void Awake()
    {
        target = FindAnyObjectByType<PlayerKaijuStats>(); // Updated to non-obsolete method
        attackPoint = transform; // Use the transform if attackPoint is not set
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
        if (target == null) return;

        // Apply damage
        target.TakeDamage(kaijuData.Damage);

        // Visual feedback
        Debug.Log($"Enemy kaiju attacked! Damage: {kaijuData.Damage}");

        // Cooldown
        currentAttackCooldown = kaijuData.AttackCooldown;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}

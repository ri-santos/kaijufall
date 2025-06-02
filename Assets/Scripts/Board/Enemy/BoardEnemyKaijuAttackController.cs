using UnityEngine;

public class BoardEnemyKaijuAttackController : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject kaijuData;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 1f;

    private float currentAttackCooldown;
    public PlayerKaijuStats target { get; private set; }
    public bool CanAttack { get; private set; } // Added public property

    private void Awake()
    {
        target = FindAnyObjectByType<PlayerKaijuStats>(); // Updated to non-obsolete method
        attackPoint = transform; // Use the transform if attackPoint is not set
    }

    private void Update()
    {
        target = FindFirstObjectByType<PlayerKaijuStats>(); // Updated to non-obsolete method
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

using UnityEngine;

[RequireComponent(typeof(PlayerKaijuMovement))]
public class PlayerKaijuRangedAttackController : MonoBehaviour
{
    [SerializeField] private PlayerKaijuScriptableObject kaijuData;
    [SerializeField] private Transform attackPoint;
    private float attackRadius;

    private float currentAttackCooldown;
    public GameObject target { get; private set; } // Changed to GameObject for flexibility
    private BoardEnemyStats smallKaijuTarget;
    private BigKaiju bigKaijuTarget;
    private int targetType; // 0 for small kaiju, 1 for big kaiju

    private Projectile projectile;
    public GameObject ProjectilePrefab; // Reference to the projectile prefab

    public bool CanAttack { get; private set; } // Added public property

    private void Awake()
    {
        bigKaijuTarget = FindFirstObjectByType<BigKaiju>(); // Updated to non-obsolete method
        attackRadius = kaijuData.AttackRange; // Set attack radius from kaiju data
        attackPoint = attackPoint ?? transform; // Use the transform if attackPoint is not set
    }

    private void Update()
    {
        smallKaijuTarget = FindFirstObjectByType<BoardEnemyStats>(); // Updated to non-obsolete method
        target = smallKaijuTarget != null ? smallKaijuTarget.gameObject : bigKaijuTarget?.gameObject;
        targetType = smallKaijuTarget != null ? 0 : 1; // Determine target type

        float distance = Vector2.Distance(transform.position, target.transform.position);
        CanAttack = distance <= kaijuData.AttackRange;

        if (currentAttackCooldown > 0)
        {
            currentAttackCooldown -= Time.deltaTime;
            return;
        }

        if (CanAttack)
        {
            switch (targetType)
            {
                case 0: // Small Kaiju
                    AttackSmall();
                    break;
                case 1: // Big Kaiju
                    AttackBig();
                    break;
            }
            // Reset cooldown after attack
            currentAttackCooldown = kaijuData.AttackCooldown;
        }
    }

    private void AttackSmall()
    {
        // Apply damage
       smallKaijuTarget.TakeDamage(kaijuData.Damage);

        // Visual feedback
        Debug.Log($"{kaijuData.KaijuName} attacked! Damage: {kaijuData.Damage}");

        // Cooldown
        currentAttackCooldown = kaijuData.AttackCooldown;
    }

    private void AttackBig()
    {
        // Apply damage
        bigKaijuTarget.TakeDamage(kaijuData.Damage);
        // Visual feedback
        Debug.Log($"{kaijuData.KaijuName} attacked Big Kaiju! Damage: {kaijuData.Damage}");
        // Cooldown
        currentAttackCooldown = kaijuData.AttackCooldown;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
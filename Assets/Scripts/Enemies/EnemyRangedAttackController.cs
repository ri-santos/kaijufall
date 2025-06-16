using UnityEngine;

public class EnemyRangedAttackController : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject kaijuData;
    [SerializeField] private Transform attackPoint;
    private float attackRadius;

    private BoardEnemyProjectile projectile;
    public GameObject ProjectilePrefab; // Reference to the projectile prefab

    private float currentAttackCooldown;
    private PlayerManager target;
    public PlayerManager Target => target;

    protected bool inCooldown;
    public bool InCooldown => inCooldown; // Public property to access canAttack

    protected bool inRange;

    public bool InRange => inRange; // Public property to access inRange

    protected bool canAttack => inRange && !inCooldown;

    protected virtual void Awake()
    {
        target = FindAnyObjectByType<PlayerManager>(); // Updated to non-obsolete method
        attackPoint = transform; // Use the transform if attackPoint is not set
        attackRadius = kaijuData.AttackRange; // Set attack radius from kaiju data
    }

    protected virtual void Update()
    {
        target = FindFirstObjectByType<PlayerManager>(); // Updated to non-obsolete method
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

    private void Attack()
    {
        Debug.Log("Ranged attack incoming");
        GameObject projectile = Instantiate(ProjectilePrefab);
        projectile.transform.position = attackPoint.position;
        projectile.GetComponent<BoardEnemyProjectile>().DirectionChecker(target.transform.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}

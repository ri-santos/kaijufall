using UnityEngine;

[RequireComponent(typeof(PlayerKaijuMovement))]
public class PlayerKaijuAttackController : MonoBehaviour
{
    [SerializeField] protected PlayerKaijuScriptableObject kaijuData;
    [SerializeField] protected Transform attackPoint;
    protected float attackRadius;

    protected float currentAttackCooldown;
    protected GameObject target; // Changed to GameObject for flexibility
    public GameObject Target => target; // Public property to access target
    protected BoardEnemyStats smallKaijuTarget;
    protected BigKaiju bigKaijuTarget;
    protected int targetType; // 0 for small kaiju, 1 for big kaiju

    protected bool inCooldown;
    public bool InCooldown => inCooldown; // Public property to access canAttack

    protected bool inRange;

    public bool InRange => inRange; // Public property to access inRange

    protected bool canAttack => inRange && !inCooldown;

    protected virtual void Awake()
    {
        bigKaijuTarget = GameObject.FindGameObjectWithTag("BigKaiju").GetComponent<BigKaiju>(); // Updated to non-obsolete method
        attackRadius = kaijuData.AttackRange; // Set attack radius from kaiju data
        attackPoint = transform; // Use the transform if attackPoint is not set
    }

    protected virtual void Update()
    {
        smallKaijuTarget = FindFirstObjectByType<BoardEnemyStats>(); // Updated to non-obsolete method
        target = smallKaijuTarget != null ? smallKaijuTarget.gameObject : bigKaijuTarget?.gameObject;

        float distance = Vector2.Distance(transform.position, target.transform.position);
        Debug.Log($"Distance to target: {currentAttackCooldown}");

        currentAttackCooldown -= Time.deltaTime;
        inCooldown = currentAttackCooldown > 0;

        inRange = distance <= attackRadius;

        Debug.Log(canAttack);
       
    }

    protected virtual void AttackSmall()
    {
        // Apply damage
       smallKaijuTarget.TakeDamage(kaijuData.Damage);

        // Visual feedback
        Debug.Log($"{kaijuData.KaijuName} attacked! Damage: {kaijuData.Damage}");

        // Cooldown
        currentAttackCooldown = kaijuData.AttackCooldown;
    }

    protected virtual void AttackBig()
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
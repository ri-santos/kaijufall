using UnityEngine;

public class BigKaijuAttackController : MonoBehaviour
{
    [SerializeField] protected BigKaijuScriptableObject kaijuData;
    [SerializeField] protected Transform attackPoint;
    public float attackRadius;

    protected float currentAttackCooldown;
    protected float currentTimeBetweenProjectiles;

    protected bool inCooldown;
    protected bool isShooting;
    protected int numProjectiles;
    public bool InCooldown => inCooldown; // Public property to access canAttack

    [Header("Attack Prefabs")]
    [SerializeField] protected BigKaijuSmallProjectile smallProjectile;
    [SerializeField] protected GameObject attackPrefab2;

    protected virtual void Awake()
    {
        attackPoint = transform; // Use the transform if attackPoint is not set
        attackRadius = kaijuData.AttackRange; // Set attack radius from kaiju data
        currentAttackCooldown = kaijuData.AttackCooldown; // Initialize cooldown
    } 
    
    // Update is called once per frame
    protected virtual void Update()
    {
        currentAttackCooldown -= Time.deltaTime;
        inCooldown = currentAttackCooldown > 0;
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}

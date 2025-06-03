using UnityEngine;

[RequireComponent(typeof(PlayerKaijuMovement))]
public class PlayerKaijuRangedAttackController : PlayerKaijuAttackController
{
    private Projectile projectile;
    public GameObject ProjectilePrefab; // Reference to the projectile prefab

    protected override void Awake()
    {
        base.Awake(); // Call the base Start method
    }

    protected override void Update()
    {
        base.Update(); // Call the base Update method

        if (canAttack)
        {
            Attack();
            // Reset cooldown after attack
            currentAttackCooldown = kaijuData.AttackCooldown;
        }
    }

    private void Attack()
    {
        GameObject projectile = Instantiate(ProjectilePrefab);
        projectile.transform.position = attackPoint.position;
        projectile.GetComponent<Projectile>().DirectionChecker(target.transform.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
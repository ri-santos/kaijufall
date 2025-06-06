using UnityEngine;

public class BoardEnemyKaijuRangedAttackController : BoardEnemyKaijuAttackController
{
    private BoardEnemyProjectile projectile;
    public GameObject ProjectilePrefab; // Reference to the projectile prefab

    protected override void Attack()
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

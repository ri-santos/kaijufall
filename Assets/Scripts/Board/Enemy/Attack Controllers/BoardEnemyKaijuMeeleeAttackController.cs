using UnityEngine;

public class BoardEnemyKaijuMeeleeAttackController : BoardEnemyKaijuAttackController
{

    // Update is called once per frame
    protected override void Attack()
    {
        if (target == null) return;

        // Apply damage
        target.TakeDamage(kaijuData.Damage);

        // Visual feedback
        Debug.Log($"Player kaiju attacked! Damage: {kaijuData.Damage}");

        // Cooldown
        currentAttackCooldown = kaijuData.AttackCooldown;
    }
}

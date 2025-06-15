using UnityEngine;
using System.Collections.Generic;
using Unity.Loading;

public class LightningRing : ProjectileWeapon
{
    List<EnemyStats> allSelectedEnemies = new List<EnemyStats>();

    protected override bool Attack(int attackCount = 1)
    {
        if (!currentStats.hitEffect) 
        {
            currentCooldown = currentStats.cooldown;
            return false;
        }

        if (!CanAttack()) return false;

        if (currentCooldown <= 0)
        {
            allSelectedEnemies = new List<EnemyStats>(FindObjectsByType<EnemyStats>(FindObjectsSortMode.None));
            currentCooldown += currentStats.cooldown;
            currentAttackCount = attackCount;
        }

        EnemyStats target = PickEnemy();
        if (target)
        {
            DamageArea(target.transform.position, currentStats.area, GetDamage());
            Instantiate(currentStats.hitEffect, target.transform.position, Quaternion.identity);
        }

        if (attackCount > 0)
        {
            currentAttackCount = attackCount - 1;
            currentAttackInterval = currentStats.projectileInterval;
        }

        return true;
    }

    EnemyStats PickEnemy()
    {
        EnemyStats target = null;
        while (!target && allSelectedEnemies.Count > 0)
        {
            int idx = Random.Range(0, allSelectedEnemies.Count);
            target = allSelectedEnemies[idx];

            if (!target)
            {
                allSelectedEnemies.RemoveAt(idx);
                continue;
            }

            Renderer r = target.GetComponent<Renderer>();
            if (!r || !r.isVisible)
            {
                allSelectedEnemies.Remove(target);
                target = null;
                continue;
            }
        }

        allSelectedEnemies.Remove(target);
        return target;
    }

    void DamageArea(Vector2 position, float radius, float damage)
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(position, radius);
        foreach (Collider2D t in targets)
        {
            EnemyStats es = t.GetComponent<EnemyStats>();
            if (es) es.TakeDamage(damage, transform.position);
        }
    }
}

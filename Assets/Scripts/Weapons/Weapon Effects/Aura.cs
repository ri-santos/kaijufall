using System.Collections.Generic;
using UnityEngine;

public class Aura : WeaponEffect
{
    Dictionary<IDamageable, float> affectedTargets = new Dictionary<IDamageable, float>();
    List<IDamageable> targetsToUnaffect = new List<IDamageable>();

    private void Update()
    {
        var affectedTargsCopy = new Dictionary<IDamageable, float>(affectedTargets);

        foreach (var pair in affectedTargsCopy)
        {
            affectedTargets[pair.Key] -= Time.deltaTime;

            if (pair.Value <= 0)
            {
                if (targetsToUnaffect.Contains(pair.Key))
                {
                    affectedTargets.Remove(pair.Key);
                    targetsToUnaffect.Remove(pair.Key);
                }
                else
                {
                    Weapon.Stats stats = weapon.GetStats();
                    affectedTargets[pair.Key] = stats.cooldown * owner.Stats.cooldown;
                    pair.Key.TakeDamage(GetDamage(), transform.position, stats.knockback);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            if (!affectedTargets.ContainsKey(damageable))
            {
                affectedTargets.Add(damageable, 0);
            }
            else if (targetsToUnaffect.Contains(damageable))
            {
                targetsToUnaffect.Remove(damageable);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            if (affectedTargets.ContainsKey(damageable))
            {
                targetsToUnaffect.Add(damageable);
            }
        }
    }
}

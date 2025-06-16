using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float damage, Vector2 sourcePosition = default, float knockbackForce = 5f, float knockbackDuration = 0.2f);
}

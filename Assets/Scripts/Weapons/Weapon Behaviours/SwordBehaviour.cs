using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : MeeleeWeaponBehaviour
{
    List<GameObject> markedEnemies;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        markedEnemies = new List<GameObject>();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !markedEnemies.Contains(collision.gameObject))
        {
            EnemyStats enemy = collision.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage(), transform.position);

            markedEnemies.Add(collision.gameObject);
        }
        else if (collision.CompareTag("Prop"))
        {
            if (collision.gameObject.TryGetComponent(out BreakableProps breakable) && !markedEnemies.Contains(collision.gameObject))
            {
                breakable.Takedamage(GetCurrentDamage());
                markedEnemies.Add(collision.gameObject);
            }
        }
    }
}

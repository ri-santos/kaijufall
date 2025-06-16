using UnityEngine;

public class FastProjectile : Projectile
{
    protected override void Start()
    {
        base.Start();
        speed = 15f;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BigKaiju") && targetTag == "BigKaiju")
        {
            collision.GetComponent<BigKaiju>().TakeDamage(damage);
        }
        if (collision.CompareTag("BoardEnemy"))
        {
            collision.GetComponent<BoardEnemyStats>().TakeDamage(damage);
        }
    }
}

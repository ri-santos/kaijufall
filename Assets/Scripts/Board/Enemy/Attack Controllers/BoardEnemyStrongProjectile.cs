using UnityEngine;

public class BoardEnemyStrongProjectile : BoardEnemyProjectile
{
    protected override void Start()
    {
        base.Start();
        speed = 15f;
    }
}

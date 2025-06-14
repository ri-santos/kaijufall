using UnityEngine;

public class BigKaijuBoardAttackController : BigKaijuAttackController
{
    protected override void Update()
    {
        base.Update();

        if (!inCooldown)
        {
            numProjectiles = Random.Range(kaijuData.BoardMinNumProjectiles, kaijuData.BoardMaxNumProjectiles + 1); // Randomly choose between 1 and 3 projectiles
            currentAttackCooldown = kaijuData.AttackCooldown + kaijuData.TimeBetweenProjectiles * numProjectiles;
        }

        isShooting = numProjectiles > 0;

        if (isShooting)
        {
            if (currentTimeBetweenProjectiles > 0)
            {
                currentTimeBetweenProjectiles -= Time.deltaTime;
                return; // Wait until the next projectile can be fired
            }

            Attack();
            numProjectiles--;
            currentTimeBetweenProjectiles = kaijuData.TimeBetweenProjectiles;
        }
    }

    protected virtual void Attack()
    {
        Debug.Log("Ranged attack incoming with " + numProjectiles + " projectiles");

        // Random angle between 225 and 315 degrees
        int angle = Random.Range(225, 315);

        // Calculate the direction based on the angle
        Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

        Debug.Log("Direction: " + direction);
        GameObject projectile = Instantiate(smallProjectile.gameObject);
        projectile.transform.position = attackPoint.position;
        projectile.GetComponent<BigKaijuSmallProjectile>().DirectionChecker(direction);
    }

}

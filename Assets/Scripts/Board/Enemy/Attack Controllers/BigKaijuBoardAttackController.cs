using UnityEngine;

public class BigKaijuBoardAttackController : BigKaijuAttackController
{
    private int attackType; // 0 or 1 for single projectile, 2 for AOE
    protected override void Update()
    {
        base.Update();

        if (!inCooldown)
        {
            attackType = Random.Range(0, 3); // Randomly choose between 0 and 2

            if (attackType != 2)
            {
                numProjectiles = Random.Range(kaijuData.BoardMinNumProjectiles, kaijuData.BoardMaxNumProjectiles + 1); // Randomly choose between 1 and 3 projectiles
                currentAttackCooldown = kaijuData.AttackCooldown + kaijuData.TimeBetweenProjectiles * numProjectiles;
                Debug.Log("num Projectiles: " + numProjectiles);
            }
            else
            {
                numProjectiles = 1; // Single projectile attack
                currentAttackCooldown = kaijuData.AttackCooldown;
            }
        }

        isShooting = numProjectiles > 0;

        if (isShooting)
        {
            switch (attackType)
            {
                case 0:
                case 1:

                    if (currentTimeBetweenProjectiles > 0)
                    {
                        currentTimeBetweenProjectiles -= Time.deltaTime;
                        return; // Wait until the next projectile can be fired
                    }

                    SingleAttack();
                    numProjectiles--;
                    currentTimeBetweenProjectiles = kaijuData.TimeBetweenProjectiles;

                    break;

                case 2:

                    AOEAttack();
                    numProjectiles--;
                    currentTimeBetweenProjectiles = kaijuData.TimeBetweenProjectiles;

                    break;
            }
        }
    }

    private void SingleAttack()
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

    private void AOEAttack()
    {
        Debug.Log("AOE attack incoming with " + 7 + " projectiles");
        // Random angle between 0 and 360 degrees
        for (int angle = 200; angle < 340; angle += 140/7)
        {
            // Calculate the direction based on the angle
            Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            Debug.Log("Direction: " + direction);
            GameObject projectile = Instantiate(smallProjectile.gameObject);
            projectile.transform.position = attackPoint.position;
            projectile.GetComponent<BigKaijuSmallProjectile>().DirectionChecker(direction);
        }

    }
}

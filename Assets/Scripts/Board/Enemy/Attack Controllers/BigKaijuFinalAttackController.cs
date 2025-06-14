using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BigKaijuFinalAttackController : BigKaijuAttackController
{
    [SerializeField] private Transform player;

    public Transform Target => player;

    private bool inRange;

    public bool InRange => inRange; // Public property to access inRange

    private bool canAttackPlayer;


    protected override void Update()
    {
        if (FindFirstObjectByType<Player>() == null) return;
        player = FindFirstObjectByType<Player>().transform;
        if (player == null)
        {
            Debug.LogError("Player transform not found. Ensure a Player object exists in the scene.");
            return;
        }

        float distance = Vector2.Distance(transform.position, player.transform.position);
        inRange = distance <= attackRadius/2;

        base.Update();

        canAttackPlayer = inRange && !inCooldown;

        if (canAttackPlayer)
        {
            int attackType = Random.Range(0, 2); // Randomly choose between 0 and 1
            Debug.Log("Attack Type: " + attackType);
            switch (attackType)
            {
                case 0:
                    SingleProjectileAttack();
                    break;
                case 1:
                    CircleProjectileAttack();
                    break;
                default:
                    Debug.LogError("Invalid attack type selected: " + attackType);
                    break;
            }
            currentAttackCooldown = kaijuData.AttackCooldown;
        }
    }

    private void SingleProjectileAttack()
    {
        GameObject projectile = Instantiate(smallProjectile.gameObject);
        projectile.transform.position = attackPoint.position;
        projectile.GetComponent<BigKaijuSmallProjectile>().DirectionChecker(player.transform.position - transform.position);
    }

    private void CircleProjectileAttack()
    {
        Debug.Log("Circle attack incoming");
        // Random angle between 0 and 360 degrees
        for (int angle = 0; angle < 360; angle += 360 / kaijuData.FinalMaxNumProjectiles)
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

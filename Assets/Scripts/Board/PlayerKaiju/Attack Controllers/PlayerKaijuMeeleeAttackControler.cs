using UnityEngine;

public class PlayerKaijuMeeleeAttackControler : PlayerKaijuAttackController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        targetType = smallKaijuTarget != null ? 0 : 1; // Determine target type

        if (canAttack)
        {
            switch (targetType)
            {
                case 0: // Small Kaiju
                    AttackSmall();
                    break;
                case 1: // Big Kaiju
                    AttackBig();
                    break;
            }
            // Reset cooldown after attack
            currentAttackCooldown = kaijuData.AttackCooldown;
        }
    }
}

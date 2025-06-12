using UnityEngine;

public class HealthPack : PickUp
{
    public int healthVal;
    public override void Collect()
    {
        if(hasBeenCollected)
        {
            return;
        }
        else
        {
            base.Collect();
        }
            PlayerManager player = FindAnyObjectByType<PlayerManager>();
        player.RestoreHealth(healthVal);
        player.UpdateHealthBar();
    }
}

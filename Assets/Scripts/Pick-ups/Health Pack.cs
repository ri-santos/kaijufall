using UnityEngine;

public class HealthPack : PickUp, ICollectible
{
    public int healthVal;
    public void Collect()
    {
        PlayerManager player = FindAnyObjectByType<PlayerManager>();
        player.RestoreHealth(healthVal);
    }
}

using UnityEngine;

public class HealthPack : MonoBehaviour, ICollectible
{
    public int healthVal;
    public void Collect()
    {
        PlayerManager player = FindAnyObjectByType<PlayerManager>();
        player.RestoreHealth(healthVal);
        Destroy(gameObject);
    }
}

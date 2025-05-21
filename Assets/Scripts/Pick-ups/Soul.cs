using UnityEngine;

public class Soul : MonoBehaviour, ICollectible
{
    public int experienceGranted;
    public int currencyGranted;

    public void Collect()
    {
        PlayerManager player = FindAnyObjectByType<PlayerManager>();
        player.IncreaseExperience(experienceGranted);
        player.AddMoney(currencyGranted);
        Destroy(gameObject);
    }
      
}

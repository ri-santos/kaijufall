using UnityEngine;

public class Soul : PickUp, ICollectible
{
    public int experienceGranted;
    public int currencyGranted;

    public void Collect()
    {
        //Debug.Log("called");
        PlayerManager player = FindAnyObjectByType<PlayerManager>();
        player.IncreaseExperience(experienceGranted);
        player.AddMoney(currencyGranted);
    }
}

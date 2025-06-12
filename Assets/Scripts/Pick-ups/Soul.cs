using UnityEngine;

public class Soul : PickUp
{
    public int experienceGranted;
    public int currencyGranted;

    public override void Collect()
    {
        if (hasBeenCollected)
        {
            return;
        }
        else
        {
            base.Collect();
        }

        //Debug.Log("called");
        PlayerManager player = FindAnyObjectByType<PlayerManager>();
        player.IncreaseExperience(experienceGranted);
        player.AddMoney(currencyGranted);
    }
}

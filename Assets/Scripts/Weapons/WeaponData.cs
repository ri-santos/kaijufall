using UnityEngine;


[CreateAssetMenu(fileName = "Weapon Data", menuName = "2D Top-Down Rogue-Like/Weapon Data")]
public class WeaponData : ItemData
{

    [HideInInspector] public string behaviour;
    public Weapon.Stats baseStats;
    public Weapon.Stats[] linearGrowth;
    public Weapon.Stats[] randomGrowth;

    public Weapon.Stats GetLevelData(int level)
    {
        if(level - 2 < linearGrowth.Length) return linearGrowth[level - 2];

        if(randomGrowth.Length > 0) return randomGrowth[Random.Range(0,randomGrowth.Length)];

        return new Weapon.Stats();
    }
}

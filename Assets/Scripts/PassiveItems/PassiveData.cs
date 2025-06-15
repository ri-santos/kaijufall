using UnityEngine;

[CreateAssetMenu(fileName = "Passive Data", menuName = "2D Top-Down Rogue-Like/Passive Data")]
public class PassiveData : ItemData
{
    public Passive.Modifier baseStats;
    public Passive.Modifier[] growth;

    public Passive.Modifier GetLevelData(int level)
    {
        if(level - 2 < growth.Length) return growth[level - 2];

        return new Passive.Modifier();
    }
}

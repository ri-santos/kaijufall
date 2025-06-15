using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Item : MonoBehaviour
{
    public int currentLevel = 1, maxLevel = 1;

    protected PlayerManager owner;

    public virtual void Initialise(ItemData data)
    {
        maxLevel = data.maxLevel;
        owner = FindAnyObjectByType<PlayerManager>();
    } 

    public virtual bool CanLevelUp()
    {
        return currentLevel <= maxLevel;
    }
    
    public virtual bool DoLevelUp()
    {
        return true;
    }

    public virtual void OnEquip()
    {

    }

    public virtual void OnUnequip()
    {
    }
}
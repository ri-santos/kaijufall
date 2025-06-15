using UnityEngine;

public abstract class WeaponEffect : MonoBehaviour
{
    [HideInInspector] public PlayerManager owner;
    [HideInInspector] public Weapon weapon;

    public float GetDamage()
    {
        return weapon.GetDamage();
    }
}
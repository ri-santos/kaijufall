using UnityEngine;

[CreateAssetMenu(fileName = "CharacterScriptableObject", menuName = "Scriptable Objects/Character")]
public class CharacterScriptableObject : ScriptableObject
{
    [SerializeField]
    private GameObject startingWeapon1;
    public GameObject StartingWeapon1 { get => startingWeapon1; private set => startingWeapon1 =value; }

    [SerializeField]
    private GameObject startingWeapon2;
    public GameObject StartingWeapon2 { get => startingWeapon2; private set => startingWeapon2 = value; }

    [SerializeField]
    private float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }

    [SerializeField]
    private float recovery;
    public float Recovery { get => recovery; private set => recovery = value; }

    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }

    [SerializeField]
    private float might;
    public float Might { get => might; private set => might = value; }

    [SerializeField]
    private float projectileSpeed;
    public float ProjectileSpeed { get => projectileSpeed; private set => projectileSpeed = value; }

    [SerializeField]
    private float souls;
    public float Souls { get => souls; private set => souls = value; }

    [SerializeField]
    private float magnet;
    public float Magnet { get => magnet; private set => magnet = value; }

}

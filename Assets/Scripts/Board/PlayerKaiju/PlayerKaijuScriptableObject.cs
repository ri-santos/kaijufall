using UnityEngine;

[CreateAssetMenu(fileName = "PlayerKaijuScriptableObject", menuName = "Scriptable Objects/PlayerKaijuScriptableObject")]
public class PlayerKaijuScriptableObject : ScriptableObject
{
    [SerializeField]
    private GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value; }

    [SerializeField]
    private string kaijuName;
    public string KaijuName { get => kaijuName; private set => kaijuName = value; }

    [SerializeField]
    private float speed;
    public float Speed { get => speed; private set => speed = value; }

    [SerializeField]
    private int health;
    public int Health { get => health; private set => health = value; }

    [SerializeField]
    private float damage;
    public float Damage { get => damage; private set => damage = value; }

    [SerializeField]
    private float attackCooldown;
    public float AttackCooldown { get => attackCooldown; private set => attackCooldown = value; }

    [SerializeField]
    private float attackRange;
    public float AttackRange { get => attackRange; private set => attackRange = value; }

    [SerializeField]
    private float blockStrength;
    public float BlockStrength { get => blockStrength; private set => blockStrength = value; }

    [SerializeField]
    private float cost;
    public float Cost { get => cost; private set => cost = value; }
}

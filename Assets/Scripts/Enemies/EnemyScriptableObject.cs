using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "Scriptable Objects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [SerializeField]
    private GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value; }

    [SerializeField]
    private string enemyName;
    public string EnemyName { get => enemyName; private set => enemyName = value; }

    [SerializeField]
    private float speed;
    public float Speed { get => speed; private set => speed = value; }

    [SerializeField]
    private int health;
    public int Health { get => health; private set => health = value; }

    [SerializeField]
    private int damage;
    public int Damage { get => damage; private set => damage = value; }

    [SerializeField]
    private float attackCooldown;
    public float AttackCooldown { get => attackCooldown; private set => attackCooldown = value; }

    [SerializeField]
    private float attackRange;
    public float AttackRange { get => attackRange; private set => attackRange = value; }

    [SerializeField]
    private float reward;
    public float Reward { get => reward; private set => reward = value; }

}

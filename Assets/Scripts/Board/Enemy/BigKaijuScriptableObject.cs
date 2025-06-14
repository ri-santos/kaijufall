using UnityEngine;

[CreateAssetMenu(fileName = "BigKaijuScriptableObject", menuName = "Scriptable Objects/BigKaijuScriptableObject")]
public class BigKaijuScriptableObject : ScriptableObject
{
    [SerializeField]
    private int numEnemies;
    public int NumEnemies { get => numEnemies; private set => numEnemies = value; }

    public GameObject enemyPrefab;

    [SerializeField]
    private float health;
    public float Health { get => health; private set => health = value; }

    [SerializeField]
    private float attackCooldown;
    public float AttackCooldown { get => attackCooldown; private set => attackCooldown = value; }

    [SerializeField]
    private float timeBetweenProjectiles;
    public float TimeBetweenProjectiles { get => timeBetweenProjectiles; private set => timeBetweenProjectiles = value; }

    [SerializeField]
    private float damage;
    public float Damage { get => damage; private set => damage = value; }

    [SerializeField]
    private float attackRange;
    public float AttackRange { get => attackRange; private set => attackRange = value; }

    [SerializeField]
    private float speed;
    public float Speed { get => speed; private set => speed = value; }

    [SerializeField]
    private float projectileSpeed;
    public float ProjectileSpeed { get => projectileSpeed; private set => projectileSpeed = value; }

    [SerializeField]
    private int boardMinNumProjectiles;
    public int BoardMinNumProjectiles { get => boardMinNumProjectiles; private set => boardMinNumProjectiles = value; }

    [SerializeField]
    private int boardMaxNumProjectiles;
    public int BoardMaxNumProjectiles { get => boardMaxNumProjectiles; private set => boardMaxNumProjectiles = value; }

    [SerializeField]
    private int finalMinNumProjectiles;
    public int FinalMinNumProjectiles { get => finalMinNumProjectiles; private set => finalMinNumProjectiles = value; }

    [SerializeField]
    private int finalMaxNumProjectiles;
    public int FinalMaxNumProjectiles { get => finalMaxNumProjectiles; private set => finalMaxNumProjectiles = value; }
}

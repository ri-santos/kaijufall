using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected float damage;
    protected Vector2 direction;
    protected float speed = 10f;
    protected float range;
    protected float distanceTraveled = 0f;
    protected string targetTag;

    [SerializeField] protected TrailRenderer trail;

    Rigidbody2D rb;
    public PlayerKaijuScriptableObject kaijuData;

    [Header("Visuals")]
    [SerializeField] protected GameObject impactEffect;
    [SerializeField] protected GameObject prefab; // Reference to the projectile prefab
    public GameObject Prefab { get => prefab; private set => prefab = value; }


    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        damage = kaijuData.Damage;
        range = kaijuData.AttackRange;
    }

    protected virtual void Update()
    {
        rb.linearVelocity = direction * speed;
        distanceTraveled += speed * Time.deltaTime;
        if (distanceTraveled >= range)
        {
            Destroy(gameObject);
        }
    }

    public void DirectionChecker(Vector3 targetPos, string tag)
    {
        float lookAngle = Mathf.Atan2(targetPos.y - transform.position.y, targetPos.x - transform.position.x) * Mathf.Rad2Deg;
        direction = (targetPos - transform.position).normalized;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, lookAngle - 45));
        targetTag = tag;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BigKaiju") && targetTag == "BigKaiju")
        {
            collision.GetComponent<BigKaiju>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<BoardEnemyStats>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}
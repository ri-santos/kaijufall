using UnityEngine;

public class BoardEnemyProjectile : MonoBehaviour
{
    private float damage;
    private Vector2 direction;
    private float speed = 10f;
    private float range;
    private float distanceTraveled = 0f;

    [SerializeField] private TrailRenderer trail;

    Rigidbody2D rb;
    public EnemyScriptableObject kaijuData;

    [Header("Visuals")]
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private GameObject prefab; // Reference to the projectile prefab
    public GameObject Prefab { get => prefab; private set => prefab = value; }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        damage = kaijuData.Damage;
        range = kaijuData.AttackRange;
    }

    private void Update()
    {
        rb.linearVelocity = direction * speed;
        distanceTraveled += speed * Time.deltaTime;
        if (distanceTraveled >= range)
        {
            Destroy(gameObject);
        }
    }

    public void DirectionChecker(Vector3 targetPos)
    {
        float lookAngle = Mathf.Atan2(targetPos.y - transform.position.y, targetPos.x - transform.position.x) * Mathf.Rad2Deg;
        direction = (targetPos - transform.position).normalized;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, lookAngle));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerKaiju"))
        {
            collision.GetComponent<PlayerKaijuStats>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerManager>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}
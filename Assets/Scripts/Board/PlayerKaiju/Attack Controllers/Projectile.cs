using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage;
    private Vector2 direction;
    private float speed = 10f;
    private float range;
    private float distanceTraveled = 0f;
    private string targetTag;

    [SerializeField] private TrailRenderer trail;

    Rigidbody2D rb;
    public PlayerKaijuScriptableObject kaijuData;

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

    public void DirectionChecker(Vector3 targetPos, string tag)
    {
        float lookAngle = Mathf.Atan2(targetPos.y - transform.position.y, targetPos.x - transform.position.x) * Mathf.Rad2Deg;
        direction = (targetPos - transform.position).normalized;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, lookAngle));
        targetTag = tag;
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
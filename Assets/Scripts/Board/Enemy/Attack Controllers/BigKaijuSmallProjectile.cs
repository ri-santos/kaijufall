using UnityEngine;

public class BigKaijuSmallProjectile : MonoBehaviour
{
    private float damage;
    private Vector2 direction;
    public float speed = 15f;
    private float range;
    private float distanceTraveled = 0f;

    [SerializeField] private TrailRenderer trail;

    Rigidbody2D rb;
    public BigKaijuScriptableObject kaijuData;

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
        float lookAngle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        direction = targetPos.normalized;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, lookAngle - 45));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerKaiju"))
        {
            collision.GetComponent<PlayerKaijuStats>().TakeDamage(damage);
            damage /= 2;
        }
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerManager>().TakeDamage(damage);
        }
    }
}

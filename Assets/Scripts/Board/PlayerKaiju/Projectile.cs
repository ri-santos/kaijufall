using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int damage;
    private Vector2 direction;
    private float speed = 10f;
    [SerializeField] private float destroyDelay = 3f;

    [SerializeField] private TrailRenderer trail;

    [Header("Visuals")]
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private GameObject prefab; // Reference to the projectile prefab
    public GameObject Prefab { get => prefab; private set => prefab = value; }

    public void Initialize(int projectileDamage, Vector2 projectileDirection)
    {
        damage = projectileDamage;
        direction = projectileDirection;
    }

    private void Start()
    {
        Destroy(gameObject, destroyDelay);
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BigKaiju"))
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
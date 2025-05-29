using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int damage;
    private Vector2 direction;
    private float speed = 10f;
    [SerializeField] private TrailRenderer trail;

    [Header("Visuals")]
    [SerializeField] private GameObject impactEffect;

    public void Initialize(int projectileDamage, Vector2 projectileDirection)
    {
        damage = projectileDamage;
        direction = projectileDirection;
        Destroy(gameObject, 3f); // Auto-destroy after 3 seconds
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<BigKaiju>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (trail != null) trail.Clear();
    }

}
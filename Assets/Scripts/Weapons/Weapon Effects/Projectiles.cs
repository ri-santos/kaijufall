using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectiles : WeaponEffect
{

    public enum DamageSource { projectile, owner }
    public DamageSource damageSource = DamageSource.projectile;
    public bool hasAutoAim = false;
    public Vector3 rotationSpeed = new Vector3(0, 0, 0);

    protected Rigidbody2D rb;
    protected int piercing;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Weapon.Stats stats = weapon.GetStats();
        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            rb.angularVelocity = rotationSpeed.z;
            rb.linearVelocity = transform.right * stats.speed;
        }

        float area = stats.area == 0 ? 1 : stats.area;
        transform.localScale = new Vector3(
            area * Mathf.Sign(transform.localScale.x),
            area * Mathf.Sign(transform.localScale.y)
        );

        piercing = stats.piercing;

        if (stats.lifespan > 0) Destroy(gameObject, stats.lifespan);
        if (hasAutoAim) AcquireAutoAimFacing();
    }

    public virtual void AcquireAutoAimFacing()
    {
        float aimAngle;

        // Fix for CS1501: Provide required arguments to FindObjectsByType
        EnemyStats[] targets = FindObjectsByType<EnemyStats>(FindObjectsSortMode.None);

        if (targets.Length > 0)
        {
            EnemyStats selectedTarget = targets[Random.Range(0, targets.Length)];
            Vector2 difference = selectedTarget.transform.position - transform.position;
            aimAngle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }
        else
        {
            aimAngle = Random.Range(0f, 360f);
        }

        transform.rotation = Quaternion.Euler(0, 0, aimAngle);
    }

    protected virtual void FixedUpdate()
    {
        if (rb.bodyType == RigidbodyType2D.Kinematic)
        {
            Weapon.Stats stats = weapon.GetStats();
            transform.position += transform.right * stats.speed * Time.fixedDeltaTime;
            rb.MovePosition(transform.position);
            transform.Rotate(rotationSpeed * Time.fixedDeltaTime);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        EnemyStats es = other.GetComponent<EnemyStats>();
        BreakableProps p = other.GetComponent<BreakableProps>();

        if (es)
        {
            Vector3 source = damageSource == DamageSource.owner && owner ? owner.transform.position : transform.position;

            es.TakeDamage(GetDamage(), source);

            Weapon.Stats stats = weapon.GetStats();
            piercing--;
            if (stats.hitEffect)
            {
                Destroy(Instantiate(stats.hitEffect, transform.position, Quaternion.identity), 5f);
            }
        }
        else if (p)
        {
            p.Takedamage(GetDamage());
            piercing--;
            Weapon.Stats stats = weapon.GetStats();
            if (stats.hitEffect)
            {
                Destroy(Instantiate(stats.hitEffect, transform.position, Quaternion.identity), 5f);
            }
        }
        if (piercing <= 0)
        {
            Destroy(gameObject);
        }
    }


    //protected Vector2 direction;
    //protected float lookAngle;
    //public void DirectionChecker()
    //{
    //    Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //    lookAngle = Mathf.Atan2(worldMousePos.y - transform.position.y, worldMousePos.x - transform.position.x) * Mathf.Rad2Deg;
    //    direction = worldMousePos - transform.position;
    //    direction.Normalize();
    //    transform.rotation = Quaternion.Euler(new Vector3(0, 0, lookAngle));
    //}
}

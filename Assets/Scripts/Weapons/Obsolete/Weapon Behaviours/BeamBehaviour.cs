using UnityEngine;

public class BeamBehaviour : ProjectileWeaponBehaviour
{
    Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.linearVelocity = direction * currentSpeed;
    }
}

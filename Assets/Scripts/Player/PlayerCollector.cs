using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerCollector : MonoBehaviour
{
    PlayerManager player;
    CircleCollider2D detector;
    public float pullSpeed;

    private void Start()
    {
        player = GetComponentInParent<PlayerManager>();
    }

    public void SetRadius(float r)
    {
        if (!detector) detector = GetComponent<CircleCollider2D>();
        detector.radius = r;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out PickUp p))
        {
            p.Collect(player, pullSpeed);
        }   
    }
}

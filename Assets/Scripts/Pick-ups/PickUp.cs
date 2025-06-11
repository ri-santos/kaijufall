using UnityEngine;

public class PickUp : MonoBehaviour, ICollectible
{

    protected bool hasBeenCollected = false;

    public virtual void Collect()
    {
        hasBeenCollected = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject, 0.3f);
        }
    }
}

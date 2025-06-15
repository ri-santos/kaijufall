using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class PickUp : MonoBehaviour
{

    public float lifespan = 0.5f;
    protected PlayerManager target;
    protected float speed;
    Vector2 initialPosition;
    float initialOffset;

    [Header("Bonuses")]
    public int experience;
    public int money;
    public int health;

    [System.Serializable]
    public struct BobbingAnimation
    {
        public float frequency;
        public Vector2 direction;
    }

    public BobbingAnimation bobbingAnimation = new BobbingAnimation
    {
        frequency = 2f,
        direction = new Vector2(0, 0.3f)
    };

    protected virtual void Start()
    {
        initialPosition = transform.position;
        initialOffset = Random.Range(0, bobbingAnimation.frequency);
    }
    protected virtual void Update()
    {
        if (target)
        {
            Vector2 distance = target.transform.position - transform.position;
            if (distance.sqrMagnitude > speed * speed * Time.deltaTime)
            {
                transform.position += (Vector3)distance.normalized * speed * Time.deltaTime;
            } else
            {
                Destroy(gameObject);
            }

        }
        else
        {
            transform.position = initialPosition + bobbingAnimation.direction * Mathf.Sin((Time.time + initialOffset) * bobbingAnimation.frequency);
        }
    }

    public virtual bool Collect(PlayerManager target, float speed, float lifespan = 0f)
    {
        if (!this.target)
        {
            this.target = target;
            this.speed = speed;
            if (lifespan > 0f)
            {
                this.lifespan = lifespan;
            }
            Destroy(gameObject, Mathf.Max(0.01f,this.lifespan));
            return true;
        }
        return false;
    }

    protected virtual void OnDestroy()
    {
        if (!target) return;
        if (experience != 0)
        {
            target.IncreaseExperience(experience);
            target.AddMoney(money);
        }
        if (health != 0)
        {
            target.RestoreHealth(health);
            //target.UpdateHealthBar();
        }
    }   
}

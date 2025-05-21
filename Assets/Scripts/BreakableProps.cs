using Unity.Burst;
using UnityEngine;

public class BreakableProps : MonoBehaviour
{
    public float health;
    
    public void Takedamage(float dmg)
    {
        health -= dmg;  
        if (health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}

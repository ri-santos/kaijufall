using UnityEngine;

public class BobbingAnimation : MonoBehaviour
{
    public float frequency; // Frequency of the bobbing motion
    public float magnitude; // range of the bobbing motion
    public Vector3 direction; // Direction of the bobbing motion
    Vector3 initialPosition;
    PickUp pickup; // Reference to the PickUp script

    private void Start()
    {
        pickup = GetComponent<PickUp>();
        //Save the initial starting position of the object
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (pickup && !pickup.hasBeenCollected)
        {
            // Sine function to create a bobbing effect
            transform.position = initialPosition + direction * Mathf.Sin(Time.time * frequency) * magnitude;
        }
       
    }
}

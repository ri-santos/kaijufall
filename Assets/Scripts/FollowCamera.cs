using UnityEngine;

public class FollowCamera1 : MonoBehaviour
{
    public Transform target;
    [SerializeField] public Vector3 offset;

    private void Awake()
    {
        if (target == null)
        {
            Debug.LogWarning("Camera target not assigned!");
            // Optionally find a default target
            target = GameObject.FindWithTag("Player")?.transform;
        }
        target = FindAnyObjectByType<Player>().transform;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
    }
}

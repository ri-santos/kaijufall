using UnityEngine;

public class FollowCamera1 : MonoBehaviour
{
    public Transform target;
    [SerializeField] public Vector3 offset;

    private void Awake()
    {
        target = FindAnyObjectByType<Player>().transform;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
    }
}

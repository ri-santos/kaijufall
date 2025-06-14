using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform playerTarget;
    public Transform boardTarget;
    public Transform cameraTarget;
    [SerializeField] public Vector3 offset;
    private Camera cameraController => GetComponent<Camera>();

    private void Start()
    {
        playerTarget = FindAnyObjectByType<Player>().transform;
        cameraTarget = playerTarget;
        GameManager.instance.onChangeToBoard += ChangeToBoardCamera;
        GameManager.instance.onChangeToPlayer += ChangeToPlayerCamera;
        GameManager.instance.onChangeToFinal += ChangeToPlayerCamera;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = cameraTarget.position + offset;
    }
    public void ChangeToBoardCamera()
    {
        Debug.Log("Changing to board camera");
        cameraTarget = boardTarget;
        offset = new Vector3(0, 0, -10);
        cameraController.orthographicSize = 7f; // Adjust the orthographic size as needed
    }

    public void ChangeToPlayerCamera()
    {
        Debug.Log("Changing to player camera");
        cameraTarget = playerTarget;
        offset = new Vector3(0, 0, -10);
        cameraController.orthographicSize = 5f; // Adjust the orthographic size as needed
    }
}

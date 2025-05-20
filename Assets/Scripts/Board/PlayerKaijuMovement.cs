using UnityEngine;

public class PlayerKaijuMovement : MonoBehaviour
{
    public PlayerKaijuScriptableObject playerKaijuData;
    Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = FindFirstObjectByType<BigKaiju>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, playerKaijuData.Speed * Time.deltaTime);
    }
}

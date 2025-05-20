using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerKaijuSpawner : MonoBehaviour
{
    GameObject kaijuPrefab;
    //InputManager inputManager;
    //InputAction spawnKaijuAction;

    //private void Awake()
    //{
    //    Debug.Log("Awake PlayerKaijuSpawner");
    //    inputManager = new InputManager();
    //    spawnKaijuAction = inputManager.Player.Spawn;
    //    spawnKaijuAction.performed += ctx => SpawnKaiju();
    //}

    public void setKaiju(GameObject kaijuPrefab)
    {
        Debug.Log("Setting kaiju prefab"); 
        this.kaijuPrefab = kaijuPrefab;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && kaijuPrefab != null)
        {
            Debug.Log("Mouse button down");
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Spawning kaiju");
                Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Instantiate(kaijuPrefab, worldMousePos, Quaternion.identity);
            }
        }
    }

    //private void OnEnable()
    //{
    //    spawnKaijuAction.Enable();
    //}
    //private void OnDisable()
    //{
    //    spawnKaijuAction.Disable();
    //}
}

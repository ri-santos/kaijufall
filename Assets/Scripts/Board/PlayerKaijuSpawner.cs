using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerKaijuSpawner : MonoBehaviour
{
    GameObject kaijuPrefab;
    public System.Action OnKaijuSpawned;

    public void setKaiju(GameObject kaijuPrefab)
    {
        Debug.Log("Setting kaiju prefab"); 
        this.kaijuPrefab = kaijuPrefab;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && kaijuPrefab != null)
        {
            Tile t = GetTileUnder();
            if (t != null && !t.isOccupied())
            {
                Debug.Log("Mouse button down");
                
                Debug.Log("Spawning kaiju");

                Instantiate(kaijuPrefab, t.transform.position, Quaternion.identity);
                t.setOccupied(true);
                OnKaijuSpawned?.Invoke();
            }
        }
    }

    private Tile GetTileUnder()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            Tile t = hit.collider.GetComponent<Tile>();
            if (t != null)
            {
                return t;
            }
        }

        return null;
    }
}

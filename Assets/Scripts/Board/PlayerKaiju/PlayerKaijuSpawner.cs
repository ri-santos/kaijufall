using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using NUnit.Framework;
using System.Collections.Generic;

public class PlayerKaijuSpawner : MonoBehaviour
{
    GameObject kaijuPrefab;
    ShopUI shopUI;
    public System.Action OnKaijuSpawned;
    Tile selectedTile;

    List<GameObject> kaijus;

    private void Start()
    {
        kaijus = new List<GameObject>();
        shopUI = FindFirstObjectByType<ShopUI>();
        if (shopUI == null)
        {
            Debug.LogError("ShopUI not found in the scene.");
        }
        else
        {
            shopUI.AcceptPurchase += SpawnKaiju;
        }
    }

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
                selectedTile = t;
                OnKaijuSpawned?.Invoke();
            }
        }
    }

    private void SpawnKaiju()
    {
        selectedTile.setOccupied(true);
        kaijuPrefab.GetComponent<PlayerKaijuMovement>().enabled = false;
        kaijuPrefab.GetComponent<PlayerKaijuAttackController>().enabled = false;
        GameObject newKaiju = Instantiate(kaijuPrefab, selectedTile.transform.position, Quaternion.identity);
        kaijus.Add(newKaiju);
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

    public void StartFight()
    {
        foreach (var kaiju in kaijus)
        {
            kaiju.GetComponent<PlayerKaijuMovement>().enabled = true;
            kaiju.GetComponent<PlayerKaijuAttackController>().enabled = true;
        }
        kaijus.Clear();
    }
}

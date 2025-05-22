using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;

    [SerializeField] private Tile _tilePrefab;

    [SerializeField] private Transform _cam;

    private Dictionary<Vector2, Tile> _tiles;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        for (float x = 0; x < _width; x++)
        {
            for (float y = 0; y < _height; y++)
            {
                Tile spawnedTile;
                if (y % 2 == 0)
                {
                    spawnedTile = Instantiate(_tilePrefab, new Vector3(x - 0.5f - _width / 2, y - _height / 1.5f), Quaternion.identity);
                }
                else
                {
                    spawnedTile = Instantiate(_tilePrefab, new Vector3(x - _width / 2, y - _height / 1.5f), Quaternion.identity);
                }

                spawnedTile.name = $"Tile {x} {y}";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);


                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        //_cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2, -10);
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }

    public void StartBattle()
    {
        foreach (var tile in _tiles.Values)
        {
            tile.gameObject.SetActive(false);
        }
    }
}
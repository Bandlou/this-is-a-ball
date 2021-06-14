using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bdl.Utils.Grid;

public class GameManager : MonoBehaviour
{
    // PUBLIC FIELDS
    public TilemapRenderer tilemapRenderer;
    public TilemapCollisionRenderer tilemapCollisionRenderer;

    // PRIVATE FIELDS
    private Tilemap tilemap;

    // LIFECYCLE

    private void Start()
    {
        tilemap = new Tilemap(10, 10, 1, new Vector3(0, -5));
        tilemap.SetTilemapRenderer(tilemapRenderer);
        tilemap.SetTilemapCollisionRenderer(tilemapCollisionRenderer);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            Save();
        if (Input.GetKeyDown(KeyCode.B))
            Load();

        if (Input.GetMouseButtonDown(0))
        {
            var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tilemap.SetTilemapSprite(mouseWorldPosition, TilemapType.GroundA);
        }
        if (Input.GetMouseButtonDown(1))
        {
            var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tilemap.SetTilemapSprite(mouseWorldPosition, TilemapType.GroundB);
        }
    }

    // PRIVATE METHODS

    private void Save()
    {
        tilemap.Save("gm");
    }

    private void Load()
    {
        tilemap.Load("gm");
    }
}
